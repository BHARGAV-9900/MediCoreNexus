using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class PrescriptionConfiguration
    : IEntityTypeConfiguration<Prescription>
{
    public void Configure(
        EntityTypeBuilder<Prescription> builder)
    {
        // =========================================================
        // Table
        // =========================================================

        builder.ToTable("Prescriptions");


        // =========================================================
        // Primary Key
        // =========================================================

        builder.HasKey(p => p.Id);


        // =========================================================
        // PublicId
        // =========================================================

        builder.Property(p => p.PublicId)
               .IsRequired();


        // =========================================================
        // Instructions
        // =========================================================

        builder.Property(p => p.Instructions)
               .IsRequired()
               .HasMaxLength(4000);


        // =========================================================
        // Notes
        // =========================================================

        builder.Property(p => p.Notes)
               .HasMaxLength(2000);


        // =========================================================
        // Appointment Relationship
        // One Appointment -> One Prescription
        // =========================================================

        builder.HasOne(p => p.Appointment)
               .WithOne(a => a.Prescription)
               .HasForeignKey<Prescription>(
                   p => p.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);


        // =========================================================
        // Prescription Items Relationship
        // One Prescription -> Many Prescription Items
        // =========================================================

        builder.HasMany(p => p.PrescriptionItems)
               .WithOne(pi => pi.Prescription)
               .HasForeignKey(pi => pi.PrescriptionId)
               .OnDelete(DeleteBehavior.Cascade);


        // =========================================================
        // Indexes
        // =========================================================

        // Only active prescriptions must have a unique AppointmentId.
        // Soft-deleted prescriptions are excluded from this rule.
        builder.HasIndex(p => p.AppointmentId)
               .IsUnique()
               .HasFilter("[IsDeleted] = 0");


        // PublicId must always be unique.
        builder.HasIndex(p => p.PublicId)
               .IsUnique();
    }
}