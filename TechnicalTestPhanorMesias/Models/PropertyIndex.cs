using TechnicalTestPhanorMesias.Models.DTOs;

namespace TechnicalTestPhanorMesias.Models
{
    public class PropertyIndex
    {
        public List<TbPropertyDTO> Properties { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CodeInternal { get; set; } = null!;

        public int? Year { get; set; }

        public int IdOwner { get; set; }

        public string OwnerName { get; set; } = null!;

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
