using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // Table
        builder.ToTable("Payments");

        // Primary Key
        builder.HasKey(p => p.Id);

        // PublicId
        builder.Property(p => p.PublicId)
               .IsRequired();

        // Amount
        builder.Property(p => p.Amount)
               .IsRequired()
               .HasPrecision(18, 2);

        // Payment Method
        builder.Property(p => p.PaymentMethod)
               .IsRequired();

        // Paid On
        builder.Property(p => p.PaidOn)
               .IsRequired();

        // Relationship
        builder.HasOne(p => p.Bill)
               .WithMany(b => b.Payments)
               .HasForeignKey(p => p.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(p => p.BillId);

        builder.HasIndex(p => p.PaidOn);

        builder.HasIndex(p => p.PublicId)
               .IsUnique();
    }
}