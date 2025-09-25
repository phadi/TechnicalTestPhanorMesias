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

            return tbPropertyDTOs;
        }

        public async Task<TbProperty> GetPropertyById(int id)
        {
            var tbProperty = await _context.TbProperties.FindAsync(id);
            if (tbProperty == null)
            {
                throw new DllNotFoundException();
            }

            return tbProperty;
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

        public DbSet<TbProperty> GetTbProperties()
        {
            return _context.TbProperties;
        }
    }
}
