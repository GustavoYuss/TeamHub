using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class studentsession
{
    public int Id { get; set; }

    public int IdStudent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string IPAdress { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual student IdStudentNavigation { get; set; } = null!;
}
