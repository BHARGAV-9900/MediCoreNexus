using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        // Table
        builder.ToTable("MedicalRecords");

        // Primary Key
        builder.HasKey(m => m.Id);

        // PublicId
        builder.Property(m => m.PublicId)
               .IsRequired();

        // Diagnosis
        builder.Property(m => m.Diagnosis)
               .IsRequired()
               .HasMaxLength(500);

        // Symptoms
        builder.Property(m => m.Symptoms)
               .IsRequired()
               .HasMaxLength(2000);

        // Clinical Notes
        builder.Property(m => m.ClinicalNotes)
               .HasMaxLength(4000);

        // Treatment Plan
        builder.Property(m => m.TreatmentPlan)
               .HasMaxLength(4000);

        // Follow Up
        builder.Property(m => m.FollowUpInstructions)
               .HasMaxLength(2000);

        // One-to-One
        builder.HasOne(m => m.Appointment)
               .WithOne(a => a.MedicalRecord)
               .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // Unique Index
        builder.HasIndex(m => m.AppointmentId)
               .IsUnique()
               .HasFilter("[IsDeleted] = 0");

        builder.HasIndex(m => m.PublicId)
               .IsUnique();
    }
}