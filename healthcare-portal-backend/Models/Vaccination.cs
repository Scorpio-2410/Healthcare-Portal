using System;
using System.Collections.Generic;

namespace Healthcare_Patient_Portal.Models;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public int MedicalRecordId { get; set; }

    public string VaccinationName { get; set; } = null!;

    public int Dose { get; set; }

    public DateOnly VaccinationDate { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
