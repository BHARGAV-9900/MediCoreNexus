using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // Table
        builder.ToTable("Patients");

        // Primary Key
        builder.HasKey(p => p.Id);

        // PublicId
        builder.Property(p => p.PublicId)
               .IsRequired();

        // First Name
        builder.Property(p => p.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        // Last Name
        builder.Property(p => p.LastName)
               .IsRequired()
               .HasMaxLength(100);

        // Date Of Birth
        builder.Property(p => p.DateOfBirth)
               .IsRequired();

        // Gender Enum
        builder.Property(p => p.Gender)
               .IsRequired();

        // Blood Group Enum
        builder.Property(p => p.BloodGroup)
               .IsRequired();

        // Phone
        builder.Property(p => p.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        // Email
        builder.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(150);

        // Address
        builder.Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(300);

        // Emergency Contact Name
        builder.Property(p => p.EmergencyContactName)
               .IsRequired()
               .HasMaxLength(100);

        // Emergency Contact Phone
        builder.Property(p => p.EmergencyContactPhone)
               .IsRequired()
               .HasMaxLength(20);

        // Insurance Number
        builder.Property(p => p.InsuranceNumber)
               .HasMaxLength(50);

        // Active Status
        builder.Property(p => p.IsActive)
               .IsRequired();

        // Relationship
        builder.HasMany(p => p.Appointments)
               .WithOne(a => a.Patient)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // Unique email among active/non-deleted patients.
        // Soft-deleted patients must not block reuse of the email.
        builder.HasIndex(p => p.Email)
               .IsUnique()
               .HasFilter("[IsDeleted] = 0");

        builder.HasIndex(p => p.PhoneNumber);

        builder.HasIndex(p => p.LastName);

        builder.HasIndex(p => p.PublicId)
               .IsUnique();
    }
}