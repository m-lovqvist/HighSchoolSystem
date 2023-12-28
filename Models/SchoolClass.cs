using System;
using System.Collections.Generic;

namespace EduBase1.Models;

public partial class SchoolClass
{
    public int ClassId { get; set; }

    public string? Name { get; set; }

    public int? FkemployeeId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Employee? Fkemployee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
