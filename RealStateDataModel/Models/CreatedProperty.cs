using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.Models
{
    public class CreatedProperty
    {
         public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CodeInternal { get; set; } = null!;

        public int? Year { get; set; }

        public int IdOwner { get; set; }
    }
}
