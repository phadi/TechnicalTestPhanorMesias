﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.Models
{
    public class PropertyFilters
    {
        public string? Name { get; set; } 

        public string? Address { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string? CodeInternal { get; set; } 

        public int Year { get; set; }

        public int IdOwner { get; set; }
    }
}
