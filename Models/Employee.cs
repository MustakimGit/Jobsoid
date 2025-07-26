using System;
using System.Collections.Generic;

namespace Jobsoid.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateOnly? HireDate { get; set; }
}
