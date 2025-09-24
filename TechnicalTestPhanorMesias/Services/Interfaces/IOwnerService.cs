using Microsoft.EntityFrameworkCore;
using TechnicalTestPhanorMesias.DataModel;
using TechnicalTestPhanorMesias.Models.DTOs;

namespace TechnicalTestPhanorMesias.Services.Interfaces
{
    public interface IOwnerService
    {
        public List<TbOwnerDTO> GetOwners();
    }
}
