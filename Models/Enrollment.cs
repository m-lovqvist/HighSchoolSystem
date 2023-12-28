using System;
using System.Collections.Generic;

namespace EduBase1.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public string? Grade { get; set; }

    public DateTime? SetDate { get; set; }

    public int? FkcourseId { get; set; }

    public virtual Course? Fkcourse { get; set; }
}
