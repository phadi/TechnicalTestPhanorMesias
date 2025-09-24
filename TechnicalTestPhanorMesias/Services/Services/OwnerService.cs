using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechnicalTestPhanorMesias.DataModel;
using TechnicalTestPhanorMesias.Models.DTOs;
using TechnicalTestPhanorMesias.Services.Interfaces;

namespace TechnicalTestPhanorMesias.Services.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly DbRealStateCompanyContext _context;
        public OwnerService(DbRealStateCompanyContext context)
        {
            _context = context;
        }

        public List<TbOwnerDTO> GetOwners()
        {
            List<TbOwnerDTO> owners = new List<TbOwnerDTO>();
            foreach (TbOwner tbOwner in _context.TbOwners)
            {
                TbOwnerDTO ownerDTO = TbOwnerDTO.ConvertToDTO(tbOwner);
                owners.Add(ownerDTO);
            }
            return owners; 
        }
    }
}
