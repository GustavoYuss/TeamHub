using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class taskstudent
{
    public int IdTaskStudent { get; set; }

    public int? IdTask { get; set; }

    public int? IdStudent { get; set; }

    public virtual student? IdStudentNavigation { get; set; }

    public virtual tasks? IdTaskNavigation { get; set; }
}
