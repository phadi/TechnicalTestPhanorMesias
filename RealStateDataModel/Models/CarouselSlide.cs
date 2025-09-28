using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.Models
{
    public class CarouselSlide
    {
        public required string ImageUrl { get; set; }
        public string? Caption { get; set; }
        public string? Title { get; set; }
    }
}
