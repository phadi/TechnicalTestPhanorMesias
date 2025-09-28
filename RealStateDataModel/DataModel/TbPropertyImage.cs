using System;
using System.Collections.Generic;

namespace RealStateDataModel.DataModel;

public partial class TbPropertyImage
{
    public int IdPropertyImage { get; set; }

    public int IdProperty { get; set; }

    public int IdImageType { get; set; }

    public string? FilePath { get; set; }

    public bool? Enabled { get; set; }
    public string? Caption { get; set; }
    public string? Title { get; set; }

    public virtual TbImageType IdImageTypeNavigation { get; set; } = null!;

    public virtual TbProperty IdPropertyNavigation { get; set; } = null!;
}
