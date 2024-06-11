using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class tasks
{
    public int IdTask { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? IdProject { get; set; }

    public virtual project? IdProjectNavigation { get; set; }

    public virtual ICollection<taskstudent> taskstudent { get; set; } = new List<taskstudent>();
}
