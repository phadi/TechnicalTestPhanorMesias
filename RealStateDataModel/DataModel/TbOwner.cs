using System;
using System.Collections.Generic;

namespace RealStateDataModel.DataModel;

public partial class TbOwner
{
    public string? Address { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public int IdOwner { get; set; }

    public string? Photo { get; set; }

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<TbProperty> TbProperties { get; set; } = new List<TbProperty>();
}
