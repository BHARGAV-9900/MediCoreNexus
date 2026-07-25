using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // Table Name
        builder.ToTable("Departments");

        // Primary Key
        builder.HasKey(d => d.Id);

        // PublicId
        builder.Property(d => d.PublicId)
               .IsRequired();

        // Name
        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Description
        builder.Property(d => d.Description)
               .HasMaxLength(500);

        // IsActive
        builder.Property(d => d.IsActive)
               .IsRequired();

        // Relationship
        builder.HasMany(d => d.Doctors)
               .WithOne(d => d.Department)
               .HasForeignKey(d => d.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}