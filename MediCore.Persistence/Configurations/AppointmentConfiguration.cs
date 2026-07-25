using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        // Table
        builder.ToTable("Appointments");

        // Primary Key
        builder.HasKey(a => a.Id);

        // PublicId
        builder.Property(a => a.PublicId)
               .IsRequired();

        // Appointment Date
        builder.Property(a => a.AppointmentDate)
               .IsRequired();

        // Status
        builder.Property(a => a.Status)
               .IsRequired();

        // Reason
        builder.Property(a => a.Reason)
               .IsRequired()
               .HasMaxLength(500);

        // Notes
        builder.Property(a => a.Notes)
               .HasMaxLength(1000);

        // Doctor Relationship
        builder.HasOne(a => a.Doctor)
               .WithMany(d => d.Appointments)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // Patient Relationship
        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // One-to-One MedicalRecord
        builder.HasOne(a => a.MedicalRecord)
               .WithOne(m => m.Appointment)
               .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // One-to-One Prescription
        builder.HasOne(a => a.Prescription)
               .WithOne(p => p.Appointment)
               .HasForeignKey<Prescription>(p => p.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // One-to-One Bill
        builder.HasOne(a => a.Bill)
               .WithOne(b => b.Appointment)
               .HasForeignKey<Bill>(b => b.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // One-to-Many Laboratory Orders
        builder.HasMany(a => a.LaboratoryOrders)
               .WithOne(l => l.Appointment)
               .HasForeignKey(l => l.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(a => a.AppointmentDate);

        builder.HasIndex(a => a.Status);

        builder.HasIndex(a => a.PublicId)
               .IsUnique();
    }
}