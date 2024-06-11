using System;
using System.Collections.Generic;

namespace TeamHubServiceUser.Entities;

public partial class student
{
    public int IdStudent { get; set; }

    public string? Name { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? SurName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? ProDocumentImage { get; set; }

    public virtual ICollection<projectstudent> projectstudent { get; set; } = new List<projectstudent>();

    public virtual ICollection<studentsession> studentsession { get; set; } = new List<studentsession>();

    public virtual ICollection<taskstudent> taskstudent { get; set; } = new List<taskstudent>();
}
