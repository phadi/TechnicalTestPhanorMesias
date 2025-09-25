using RealStateDataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.DTOs
{
    public class TbOwnerDTO
    {
        public string? Address { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public int IdOwner { get; set; }

        public string? Photo { get; set; }

        public DateOnly? Birthday { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public static TbOwner ConvertToData(TbOwnerDTO tbOwnerDTO)
        {
            TbOwner tbOwner = new TbOwner();
            tbOwner.Address = tbOwnerDTO.Address;
            tbOwner.LastName = tbOwnerDTO.LastName;
            tbOwner.FirstName = tbOwnerDTO.FirstName;
            tbOwner.IdOwner = tbOwnerDTO.IdOwner;
            tbOwner.Photo = tbOwnerDTO.Photo;
            tbOwner.Birthday = tbOwnerDTO.Birthday;
            tbOwner.Address = tbOwnerDTO.Address;

            return tbOwner;
        }

        public static TbOwnerDTO ConvertToDTO(TbOwner tbOwner)
        {
            TbOwnerDTO tbOwnerDTO = new TbOwnerDTO();
            tbOwnerDTO.Address = tbOwner.Address;
            tbOwnerDTO.LastName = tbOwner.LastName;
            tbOwnerDTO.FirstName = tbOwner.FirstName;
            tbOwnerDTO.IdOwner = tbOwner.IdOwner;
            tbOwnerDTO.Photo = tbOwner.Photo;
            tbOwnerDTO.Birthday = tbOwner.Birthday;
            tbOwnerDTO.Address = tbOwner.Address;

            return tbOwnerDTO;
        }
    }
}
