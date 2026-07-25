using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        // Table
        builder.ToTable("Bills");

        // Primary Key
        builder.HasKey(b => b.Id);

        // PublicId
        builder.Property(b => b.PublicId)
               .IsRequired();

        // Total Amount
        builder.Property(b => b.TotalAmount)
               .IsRequired()
               .HasPrecision(18, 2);

        // Paid Status
        builder.Property(b => b.IsPaid)
               .IsRequired();

        // One-to-One Appointment
        builder.HasOne(b => b.Appointment)
               .WithOne(a => a.Bill)
               .HasForeignKey<Bill>(b => b.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // One-to-Many Payments
        builder.HasMany(b => b.Payments)
               .WithOne(p => p.Bill)
               .HasForeignKey(p => p.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(b => b.AppointmentId)
               .IsUnique();

        builder.HasIndex(b => b.PublicId)
               .IsUnique();
    }
}