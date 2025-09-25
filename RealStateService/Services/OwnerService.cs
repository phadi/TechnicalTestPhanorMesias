using Microsoft.EntityFrameworkCore;
using RealStateDataModel.DataModel;
using RealStateDataModel.DTOs;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateService.Services
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
