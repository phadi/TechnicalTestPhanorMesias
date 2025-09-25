using RealStateDataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.DTOs
{
    public class TbPropertyDTO
    {
        public int IdProperty { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CodeInternal { get; set; } = null!;

        public int? Year { get; set; }

        public int IdOwner { get; set; }

        public virtual TbOwnerDTO Owners { get; set; } = null!;

        public static TbProperty ConvertToData(TbPropertyDTO tbPropertyDTO)
        {
            TbProperty tbProperty = new TbProperty();
            tbProperty.IdProperty = tbPropertyDTO.IdProperty;
            tbProperty.Name = tbPropertyDTO.Name;
            tbProperty.Address = tbPropertyDTO.Address;
            tbProperty.Price = tbPropertyDTO.Price;
            tbProperty.CodeInternal = tbPropertyDTO.CodeInternal;
            tbProperty.Year = tbPropertyDTO.Year;
            tbProperty.IdOwner = tbPropertyDTO.IdOwner;
            tbProperty.IdOwnerNavigation = TbOwnerDTO.ConvertToData(tbPropertyDTO.Owners);
            return tbProperty;
        }

        public static TbPropertyDTO ConvertToDTO(TbProperty tbProperty)
        {
            TbPropertyDTO tbPropertyDTO = new TbPropertyDTO();
            tbPropertyDTO.IdProperty = tbProperty.IdProperty;
            tbPropertyDTO.Name = tbProperty.Name;
            tbPropertyDTO.Address = tbProperty.Address;
            tbPropertyDTO.Price = tbProperty.Price;
            tbPropertyDTO.CodeInternal = tbProperty.CodeInternal;
            tbPropertyDTO.Year = tbProperty.Year;
            tbPropertyDTO.IdOwner = tbProperty.IdOwner;
            tbPropertyDTO.Owners = TbOwnerDTO.ConvertToDTO(tbProperty.IdOwnerNavigation);
            return tbPropertyDTO;
        }
    }
}
