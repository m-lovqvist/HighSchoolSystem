using System;
using System.Collections.Generic;

namespace EduBase1.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SocialSecurityNumber { get; set; }

    public string? Address { get; set; }

    public string? Zip { get; set; }

    public int? FkclassId { get; set; }

    public virtual SchoolClass? Fkclass { get; set; }
}
