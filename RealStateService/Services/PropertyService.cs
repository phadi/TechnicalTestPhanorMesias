using Microsoft.EntityFrameworkCore;
using RealStateDataModel.DataModel;
using RealStateDataModel.DTOs;
using RealStateDataModel.Models;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateService.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly DbRealStateCompanyContext _context;
        public PropertyService(DbRealStateCompanyContext context)
        {
            _context = context;
        }

        public async Task<List<TbPropertyDTO>> GetProperties()
        {
            var dbRealStateCompanyContext = await _context.TbProperties.Include(t => t.IdOwnerNavigation).ToListAsync();
            List<TbPropertyDTO> tbPropertyDTOs = new List<TbPropertyDTO>();
            foreach (TbProperty tbProperty in dbRealStateCompanyContext)
            {
                TbPropertyDTO tbPropertyDTO = TbPropertyDTO.ConvertToDTO(tbProperty);
                tbPropertyDTOs.Add(tbPropertyDTO);
            }
            return tbPropertyDTOs;
        }
        public async Task<List<TbPropertyDTO>> FilterProperties(PropertyIndex propertyIndexParam)
        {
            List<TbPropertyDTO> tbPropertyDTOs = await GetProperties();
            List<TbPropertyDTO> filterPropertyDTOs = new List<TbPropertyDTO>();
            FilterPropertiesmethod(propertyIndexParam, ref tbPropertyDTOs, ref filterPropertyDTOs);

            return tbPropertyDTOs;
        }

        public async Task<TbPropertyDTO> GetPropertyDTOById(int id)
        {
            var tbProperty = await _context.TbProperties.FindAsync(id);            
            if (tbProperty == null)
            {
                throw new DllNotFoundException();
            }
            TbPropertyDTO propertyDTO = TbPropertyDTO.ConvertToDTO(tbProperty);
            propertyDTO.Slides = new List<CarouselSlide>();
            var slides = await _context.TbPropertyImages.Where(i => i.IdProperty == id).ToListAsync();
            foreach(TbPropertyImage slide in slides)
            {
                string imageUrl = $"/Images/{slide.FilePath}";
                CarouselSlide carouselSlide = new CarouselSlide { ImageUrl = imageUrl, Caption = slide.Caption, Title = slide.Title };
                propertyDTO.Slides.Add(carouselSlide);
            }
            return propertyDTO;
        }

        public async Task<TbPropertyDTO> GetPropertyDetails(int id)
        {
            var tbProperty = await _context.TbProperties
                .Include(t => t.IdOwnerNavigation)
                .FirstOrDefaultAsync(m => m.IdProperty == id);

            if (tbProperty == null)
            {
                throw new DllNotFoundException();
            }

            return TbPropertyDTO.ConvertToDTO(tbProperty);
        }

        public async Task<int> CreateProperty(TbProperty tbProperty)
        {
            _context.Add(tbProperty);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateProperty(TbProperty tbProperty)
        {
            _context.Update(tbProperty);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePropertyDTO(TbPropertyDTO tbProperty)
        {
            TbProperty property = TbPropertyDTO.ConvertToData(tbProperty);
            _context.Update(property);
            return await _context.SaveChangesAsync();
        }

        public DbSet<TbProperty> GetTbProperties()
        {
            return _context.TbProperties;
        }

        public async Task<int> SaveImage(int id, string fileName)
        {
            if (TbPropertyExists(id))
            {
                TbPropertyImage image = new TbPropertyImage();
                image.FilePath = fileName;
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
            return GetTbProperties().Any(e => e.IdProperty == id);
        }

        private static void FilterPropertiesmethod(PropertyIndex propertyIndexParam, ref List<TbPropertyDTO> tbPropertyDTOs, ref List<TbPropertyDTO> filterPropertyDTOs)
        {
            if (!string.IsNullOrEmpty(propertyIndexParam.Name))
            {
                filterPropertyDTOs = tbPropertyDTOs.Where(x => x.Name.Contains(propertyIndexParam.Name)).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }

            if (!string.IsNullOrEmpty(propertyIndexParam.CodeInternal))
            {
                if (filterPropertyDTOs.Count == 0)
                    filterPropertyDTOs = tbPropertyDTOs.Where(x => x.CodeInternal.Contains(propertyIndexParam.CodeInternal)).ToList();
                else
                    filterPropertyDTOs = filterPropertyDTOs.Where(x => x.CodeInternal.Contains(propertyIndexParam.CodeInternal)).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }

            if (!string.IsNullOrEmpty(propertyIndexParam.Address))
            {
                if (filterPropertyDTOs.Count == 0)
                    filterPropertyDTOs = tbPropertyDTOs.Where(x => x.Address.Contains(propertyIndexParam.Address)).ToList();
                else
                    filterPropertyDTOs = filterPropertyDTOs.Where(x => x.Address.Contains(propertyIndexParam.Address)).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }

            if (propertyIndexParam.MinPrice != 0 && propertyIndexParam.MaxPrice != 0)
            {
                if (filterPropertyDTOs.Count == 0)
                    filterPropertyDTOs = tbPropertyDTOs.Where(x => x.Price >= propertyIndexParam.MinPrice && x.Price <= propertyIndexParam.MaxPrice).ToList();
                else
                    filterPropertyDTOs = filterPropertyDTOs.Where(x => x.Price >= propertyIndexParam.MinPrice && x.Price <= propertyIndexParam.MaxPrice).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }

            if (propertyIndexParam.Year != null && propertyIndexParam.Year != 0)
            {
                if (filterPropertyDTOs.Count == 0)
                    filterPropertyDTOs = tbPropertyDTOs.Where(x => x.Year == propertyIndexParam.Year).ToList();
                else
                    filterPropertyDTOs = filterPropertyDTOs.Where(x => x.Year == propertyIndexParam.Year).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }

            if (propertyIndexParam.IdOwner != 0)
            {
                if (filterPropertyDTOs.Count == 0)
                    filterPropertyDTOs = tbPropertyDTOs.Where(x => x.IdOwner == propertyIndexParam.IdOwner).ToList();
                else
                    filterPropertyDTOs = filterPropertyDTOs.Where(x => x.IdOwner == propertyIndexParam.IdOwner).ToList();
                if (filterPropertyDTOs.Count != 0) tbPropertyDTOs = filterPropertyDTOs;
            }
        }
    }
}
