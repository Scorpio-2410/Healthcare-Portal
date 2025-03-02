using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class HealthcareProvider
{
    public int HealthcareProviderId { get; set; }

    public int UserId { get; set; }

    public string Specialty { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual User User { get; set; } = null!;
}
