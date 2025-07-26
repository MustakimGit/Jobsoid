using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? VehicleTypeId { get; set; }

    public int Vehiclesid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual VehicleType? VehicleType { get; set; }
}
