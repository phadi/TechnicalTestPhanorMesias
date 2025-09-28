using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealStateDataModel.DataModel;
using RealStateDataModel.Models;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateService.Services
{
    public class PropertyApiService : IPropertyApiService
    {
        private readonly DbRealStateCompanyApiContext _context;
        public PropertyApiService(DbRealStateCompanyApiContext context)
        {
            _context = context;
        }
        public async Task<List<TbProperty>> GetProperties()
        {
            return await _context.TbProperties.ToListAsync();
        }

        public async Task<List<TbProperty>> GetProperties(PropertyFilters filters)
        {
            var properties = await _context.TbProperties.ToListAsync();
            var resp = (from p in properties
                        where (!string.IsNullOrEmpty(filters.Name) && p.Name.ToLowerInvariant().Contains(filters.Name.ToLowerInvariant()))
                        || (!string.IsNullOrEmpty(filters.CodeInternal) && p.CodeInternal.ToLowerInvariant().Contains(filters.CodeInternal.ToLowerInvariant()))
                        || (!string.IsNullOrEmpty(filters.Address) && p.Address.ToLowerInvariant().Contains(filters.Address.ToLowerInvariant()))
                        || (p.IdOwner == filters.IdOwner)
                        || (p.Year == filters.Year)
                        || (p.Price > filters.MinPrice && p.Price < filters.MaxPrice)
                        select p).ToList<TbProperty>();

            return resp;
        }

        public async Task<int> UpdateProperty(int id, UpdatedProperty tbProperty)
        {
            if (TbPropertyExists(id))
            {
                TbProperty updatedProperty = _context.TbProperties.Where(p => p.IdProperty == id).FirstOrDefault();
                if(!string.IsNullOrEmpty(tbProperty.Name))
                    updatedProperty.Name = tbProperty.Name;

                if (!string.IsNullOrEmpty(tbProperty.Address))
                    updatedProperty.Address = tbProperty.Address;

                if (!string.IsNullOrEmpty(tbProperty.CodeInternal))
                    updatedProperty.CodeInternal = tbProperty.CodeInternal;

                if (tbProperty.Price != null && tbProperty.Price > 0)
                    updatedProperty.Price = (decimal)tbProperty.Price;

                if (tbProperty.Year != null && tbProperty.Year > 0)
                    updatedProperty.Year = tbProperty.Year;

                if (tbProperty.IdOwner != null && tbProperty.IdOwner > 0)
                    updatedProperty.IdOwner = (int)tbProperty.IdOwner;

                _context.Update(updatedProperty);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
            
        }

        public async Task<int> ChangePrice(int id, decimal price)
        {
            if (TbPropertyExists(id))
            {
                TbProperty updatedProperty = _context.TbProperties.Where(p => p.IdProperty == id).FirstOrDefault();
                if (price != null && price > 0)
                    updatedProperty.Price = price;

                _context.Update(updatedProperty);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<TbProperty> CreateProperty(CreatedProperty tbProperty)
        {
            TbProperty newProperty = new TbProperty();
            newProperty.Name = tbProperty.Name;
            newProperty.Address = tbProperty.Address;
            newProperty.Price = tbProperty.Price;
            newProperty.Year = tbProperty.Year;
            newProperty.CodeInternal = tbProperty.CodeInternal;
            newProperty.IdOwner = tbProperty.IdOwner;

            _context.Add(newProperty);
            await _context.SaveChangesAsync();
            return newProperty;
        }

        public async Task<int> SaveImage(int id, string fileName)
        {
            if (TbPropertyExists(id))
            {
                TbPropertyImage image = new TbPropertyImage();
                image.FilePath = $"{id.ToString()}_{fileName}";
                image.IdProperty = id;
                image.IdImageType = 1;
                image.Enabled = true;
                image.Caption = fileName.Split(".")[0];
                image.Title = fileName.Split(".")[0];
                _context.Add(image);

                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        private bool TbPropertyExists(int id)
        {
            List<TbProperty> properties = GetProperties().Result;
            return properties.Any(e => e.IdProperty == id);
        }

        
    }
}
