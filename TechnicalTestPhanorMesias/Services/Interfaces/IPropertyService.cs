using Microsoft.EntityFrameworkCore;
using TechnicalTestPhanorMesias.DataModel;
using TechnicalTestPhanorMesias.Models;
using TechnicalTestPhanorMesias.Models.DTOs;

namespace TechnicalTestPhanorMesias.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<TbPropertyDTO>> GetProperties();
        Task<List<TbPropertyDTO>> FilterProperties(PropertyIndex propertyIndexParam);
        DbSet<TbProperty> GetTbProperties();
        Task<TbProperty> GetPropertyById(int id);
        Task<TbPropertyDTO> GetPropertyDetails(int id);
        Task<int> CreateProperty(TbProperty tbProperty);
        Task<int> UpdateProperty(TbProperty tbProperty);
    }
}
