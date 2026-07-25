using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        // Table
        builder.ToTable("Medicines");

        // Primary Key
        builder.HasKey(m => m.Id);

        // PublicId
        builder.Property(m => m.PublicId)
               .IsRequired();

        // Name
        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(150);

        // Manufacturer
        builder.Property(m => m.Manufacturer)
               .HasMaxLength(150);

        // Unit Price
        builder.Property(m => m.UnitPrice)
               .IsRequired()
               .HasPrecision(18, 2);

        // Is Active
        builder.Property(m => m.IsActive)
               .IsRequired();

        // Relationship
        builder.HasMany(m => m.PrescriptionItems)
               .WithOne(pi => pi.Medicine)
               .HasForeignKey(pi => pi.MedicineId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(m => m.Name);

        builder.HasIndex(m => m.PublicId)
               .IsUnique();
    }
}