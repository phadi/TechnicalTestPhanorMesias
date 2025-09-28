using Microsoft.EntityFrameworkCore;
using RealStateDataModel.DataModel;
using RealStateDataModel.DTOs;
using RealStateDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateService.Interfaces
{
    public interface IPropertyService
    {
        Task<List<TbPropertyDTO>> GetProperties();
        Task<List<TbPropertyDTO>> FilterProperties(PropertyIndex propertyIndexParam);
        DbSet<TbProperty> GetTbProperties();
        Task<TbProperty> GetPropertyById(int id);
        Task<TbPropertyDTO> GetPropertyDTOById(int id, string contentRootPath);
        Task<TbPropertyDTO> GetPropertyDetails(int id);
        Task<int> CreateProperty(TbProperty tbProperty);
        Task<int> UpdateProperty(TbProperty tbProperty);
        Task<int> UpdatePropertyDTO(TbPropertyDTO tbProperty);
    }
}
