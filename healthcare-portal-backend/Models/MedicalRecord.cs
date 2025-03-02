using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class MedicalRecord
{
    public int MedicalRecordId { get; set; }

    public int AppointmentId { get; set; }

    public int HealthcareProviderId { get; set; }

    public int PatientId { get; set; }

    public int? PrescriptionId { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Treatment { get; set; } = null!;

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual HealthcareProvider HealthcareProvider { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Prescription? Prescription { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
