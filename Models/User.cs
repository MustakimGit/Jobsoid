using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class User
{
    public int Usersid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
