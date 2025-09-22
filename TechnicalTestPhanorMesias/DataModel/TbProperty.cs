using System;
using System.Collections.Generic;

namespace TechnicalTestPhanorMesias.DataModel;

public partial class TbProperty
{
    public int IdProperty { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Price { get; set; }

    public string CodeInternal { get; set; } = null!;

    public int? Year { get; set; }

    public int IdOwner { get; set; }

    public virtual TbOwner IdOwnerNavigation { get; set; } = null!;

    public virtual ICollection<TbPropertyImage> TbPropertyImages { get; set; } = new List<TbPropertyImage>();

    public virtual ICollection<TbPropertyTrace> TbPropertyTraces { get; set; } = new List<TbPropertyTrace>();
}
