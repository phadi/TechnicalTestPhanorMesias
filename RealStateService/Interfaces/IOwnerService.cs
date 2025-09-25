using RealStateDataModel.DataModel;
using RealStateDataModel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateService.Interfaces
{
    public interface IOwnerService
    {
        public List<TbOwnerDTO> GetOwners();
    }
}
