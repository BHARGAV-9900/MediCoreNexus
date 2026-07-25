using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class LaboratoryTestConfiguration : IEntityTypeConfiguration<LaboratoryTest>
{
    public void Configure(EntityTypeBuilder<LaboratoryTest> builder)
    {
        // Table
        builder.ToTable("LaboratoryTests");

        // Primary Key
        builder.HasKey(t => t.Id);

        // PublicId
        builder.Property(t => t.PublicId)
               .IsRequired();

        // Name
        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);

        // Price
        builder.Property(t => t.Price)
               .IsRequired()
               .HasPrecision(18, 2);

        // Description
        builder.Property(t => t.Description)
               .HasMaxLength(1000);

        // Active Status
        builder.Property(t => t.IsActive)
               .IsRequired();

        // Relationship
        builder.HasMany(t => t.LaboratoryOrders)
               .WithOne(o => o.LaboratoryTest)
               .HasForeignKey(o => o.LaboratoryTestId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(t => t.Name);

        builder.HasIndex(t => t.PublicId)
               .IsUnique();
    }
}