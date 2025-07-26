using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
