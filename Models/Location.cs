using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Location
{
    public int Locationid { get; set; }

    public string? Title { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? Zip { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
