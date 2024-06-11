using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class document
{
    public int IdDocument { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    public int? Extension { get; set; }

    public int IdProject { get; set; }

    public virtual extension? ExtensionNavigation { get; set; }

    public virtual project IdProjectNavigation { get; set; } = null!;
}
