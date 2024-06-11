using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class extension
{
    public int IdExtension { get; set; }

    public string Extension1 { get; set; } = null!;

    public virtual ICollection<document> document { get; set; } = new List<document>();
}
