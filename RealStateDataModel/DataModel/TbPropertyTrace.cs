using System;
using System.Collections.Generic;

namespace RealStateDataModel.DataModel;

public partial class TbPropertyTrace
{
    public int IdPropertyTrace { get; set; }

    public int IdProperty { get; set; }

    public DateOnly DateSale { get; set; }

    public string? Name { get; set; }

    public decimal Value { get; set; }

    public decimal? Tax { get; set; }

    public virtual TbProperty IdPropertyNavigation { get; set; } = null!;
}
