using System;
using System.Collections.Generic;

namespace TechnicalTestPhanorMesias.DataModel;

public partial class TbImageType
{
    public int IdImageType { get; set; }

    public string ImageType { get; set; } = null!;

    public virtual ICollection<TbPropertyImage> TbPropertyImages { get; set; } = new List<TbPropertyImage>();
}
