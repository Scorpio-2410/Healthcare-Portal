using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int HealthcareProviderId { get; set; }

    public DateTime AppointmentDatetime { get; set; }

    public string AppointmentStatus { get; set; } = null!;

    public string AppointmentNotes { get; set; } = null!;

    public virtual HealthcareProvider HealthcareProvider { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
