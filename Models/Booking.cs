using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? VehicleId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual User? User { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
