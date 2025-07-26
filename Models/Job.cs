using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Job
{
    public int Jobid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Locationid { get; set; }

    public int? Departmentid { get; set; }

    public DateTime? Closingdate { get; set; }

    public DateTime? Posteddate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Location? Location { get; set; }
}
