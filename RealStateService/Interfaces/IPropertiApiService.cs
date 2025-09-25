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
    public interface IPropertyApiService
    {
        Task<List<TbProperty>> GetProperties();
        Task<List<TbProperty>> GetProperties(PropertyFilters filters);
        Task<int> UpdateProperty(int id, UpdatedProperty tbProperty);
        Task<int> ChangePrice(int id, decimal price);
        Task<TbProperty> CreateProperty(CreatedProperty tbProperty);
        Task<int> SaveImage(int id, string fileName);
    }
}
