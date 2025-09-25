using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.Models
{
    public class UpdatedProperty
    {
        public string? Name { get; set; }

        public string? Address { get; set; } 

        public decimal? Price { get; set; }

        public string? CodeInternal { get; set; } 

        public int? Year { get; set; }

        public int? IdOwner { get; set; }
    }
}
