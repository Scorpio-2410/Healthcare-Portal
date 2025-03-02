using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class User
{
    public int UserId { get; set; }

    public string RoleType { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public virtual HealthcareProvider? HealthcareProvider { get; set; }

    public virtual Patient? Patient { get; set; }
}
