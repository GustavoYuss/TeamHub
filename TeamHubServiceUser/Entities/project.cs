using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class project
{
    public int IdProject { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<document> document { get; set; } = new List<document>();

    public virtual ICollection<projectstudent> projectstudent { get; set; } = new List<projectstudent>();

    public virtual ICollection<tasks> tasks { get; set; } = new List<tasks>();
}
