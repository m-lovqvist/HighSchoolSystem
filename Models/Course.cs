using System;
using System.Collections.Generic;

namespace EduBase1.Models;

public partial class Course
{
    private string? startDate;

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public int? FkclassId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual SchoolClass? StartDateNavigation { get; set; }
}
