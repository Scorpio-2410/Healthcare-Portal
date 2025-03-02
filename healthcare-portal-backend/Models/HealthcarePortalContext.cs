using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_Patient_Portal.Models;

public partial class HealthcarePortalContext : DbContext
{
    public HealthcarePortalContext()
    {
    }

    public HealthcarePortalContext(DbContextOptions<HealthcarePortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<CommunicationChannel> CommunicationChannels { get; set; }

    public virtual DbSet<HealthcareProvider> HealthcareProviders { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Healthcare Portal; User Id=sa; Password=Software!123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__A50828FCDEF02ED2");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDatetime)
                .HasColumnType("datetime")
                .HasColumnName("appointment_datetime");
            entity.Property(e => e.AppointmentNotes)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("appointment_notes");
            entity.Property(e => e.AppointmentStatus)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("appointment_status");
            entity.Property(e => e.HealthcareProviderId).HasColumnName("healthcare_provider_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.HealthcareProvider).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.HealthcareProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__healt__55BFB948");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__patie__54CB950F");
        });

        modelBuilder.Entity<CommunicationChannel>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Communic__0BBF6EE612711CA8");

            entity.ToTable("CommunicationChannel");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.MessageText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("message_text");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
        });

        modelBuilder.Entity<HealthcareProvider>(entity =>
        {
            entity.HasKey(e => e.HealthcareProviderId).HasName("PK__Healthca__40621EA9458692EE");

            entity.HasIndex(e => e.UserId, "UQ__Healthca__B9BE370EDC53E390").IsUnique();

            entity.Property(e => e.HealthcareProviderId).HasColumnName("healthcare_provider_id");
            entity.Property(e => e.Specialty)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("specialty");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.HealthcareProvider)
                .HasForeignKey<HealthcareProvider>(d => d.UserId)
                .HasConstraintName("FK__Healthcar__user___4D2A7347");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordId).HasName("PK__MedicalR__05C4C30ACE7387A7");

            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.HealthcareProviderId).HasColumnName("healthcare_provider_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.Treatment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("treatment");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__appoi__5D60DB10");

            entity.HasOne(d => d.HealthcareProvider).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.HealthcareProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__healt__5E54FF49");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__patie__5F492382");

            entity.HasOne(d => d.Prescription).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__MedicalRe__presc__603D47BB");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__4D5CE476FC945111");

            entity.HasIndex(e => e.UserId, "UQ__Patients__B9BE370E9E3B8577").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.UserGender)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("user_gender");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.UserId)
                .HasConstraintName("FK__Patients__user_i__50FB042B");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__3EE444F8A75B9FA8");

            entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.HealthcareProviderId).HasColumnName("healthcare_provider_id");
            entity.Property(e => e.Medicine)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("medicine");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Refillable).HasColumnName("refillable");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescript__appoi__589C25F3");

            entity.HasOne(d => d.HealthcareProvider).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.HealthcareProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescript__healt__5A846E65");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescript__patie__59904A2C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F5ECF58CE");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.RoleType)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("role_type");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("PK__Vaccinat__E588AFE778BB86E8");

            entity.Property(e => e.VaccinationId).HasColumnName("vaccination_id");
            entity.Property(e => e.Dose).HasColumnName("dose");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.VaccinationDate).HasColumnName("vaccination_date");
            entity.Property(e => e.VaccinationName)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("vaccination_name");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("FK__Vaccinati__medic__6319B466");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
