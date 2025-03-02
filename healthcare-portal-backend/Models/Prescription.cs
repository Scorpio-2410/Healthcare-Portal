using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int HealthcareProviderId { get; set; }

    public string Medicine { get; set; } = null!;

    public int Dosage { get; set; }

    public int Frequency { get; set; }

    public bool Refillable { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual HealthcareProvider HealthcareProvider { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; set; } = null!;
}
