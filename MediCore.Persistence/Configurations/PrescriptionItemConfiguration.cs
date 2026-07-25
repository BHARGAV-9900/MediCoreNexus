using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
{
    public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
    {
        // Table
        builder.ToTable("PrescriptionItems");

        // Primary Key
        builder.HasKey(pi => pi.Id);

        // PublicId
        builder.Property(pi => pi.PublicId)
               .IsRequired();

        // Dosage
        builder.Property(pi => pi.Dosage)
               .IsRequired()
               .HasMaxLength(100);

        // Frequency
        builder.Property(pi => pi.Frequency)
               .IsRequired()
               .HasMaxLength(100);

        // Duration In Days
        builder.Property(pi => pi.DurationInDays)
               .IsRequired();

        // Quantity
        builder.Property(pi => pi.Quantity)
               .IsRequired();

        // Prescription Relationship
        builder.HasOne(pi => pi.Prescription)
               .WithMany(p => p.PrescriptionItems)
               .HasForeignKey(pi => pi.PrescriptionId)
               .OnDelete(DeleteBehavior.Cascade);

        // Medicine Relationship
        builder.HasOne(pi => pi.Medicine)
               .WithMany(m => m.PrescriptionItems)
               .HasForeignKey(pi => pi.MedicineId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(pi => pi.PrescriptionId);

        builder.HasIndex(pi => pi.MedicineId);

        builder.HasIndex(pi => pi.PublicId)
               .IsUnique();
    }
}