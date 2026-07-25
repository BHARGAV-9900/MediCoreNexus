using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // Table Name
        builder.ToTable("Doctors");

        // Primary Key
        builder.HasKey(d => d.Id);

        // PublicId
        builder.Property(d => d.PublicId)
               .IsRequired();

        // First Name
        builder.Property(d => d.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        // Last Name
        builder.Property(d => d.LastName)
               .IsRequired()
               .HasMaxLength(100);

        // Email
        builder.Property(d => d.Email)
               .IsRequired()
               .HasMaxLength(150);

        // Phone Number
        builder.Property(d => d.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        // Specialization
        builder.Property(d => d.Specialization)
               .IsRequired()
               .HasMaxLength(100);

        // Experience
        builder.Property(d => d.ExperienceYears)
               .IsRequired();

        // Money
        builder.Property(d => d.ConsultationFee)
               .IsRequired()
               .HasPrecision(18, 2);

        // Availability
        builder.Property(d => d.IsAvailable)
               .IsRequired();

        // Relationship
        builder.HasOne(d => d.Department)
               .WithMany(dep => dep.Doctors)
               .HasForeignKey(d => d.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(d => d.Email)
               .IsUnique();

        builder.HasIndex(d => d.PhoneNumber)
               .IsUnique();

        builder.HasIndex(d => d.Specialization);
    }
}