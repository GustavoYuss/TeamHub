using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class projectstudent
{
    public int IdProjectStudent { get; set; }

    public int? IdProject { get; set; }

    public int? IdStudent { get; set; }

    public virtual project? IdProjectNavigation { get; set; }

    public virtual student? IdStudentNavigation { get; set; }
}
