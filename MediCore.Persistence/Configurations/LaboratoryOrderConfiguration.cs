using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class LaboratoryOrderConfiguration : IEntityTypeConfiguration<LaboratoryOrder>
{
    public void Configure(EntityTypeBuilder<LaboratoryOrder> builder)
    {
        // Table
        builder.ToTable("LaboratoryOrders");

        // Primary Key
        builder.HasKey(o => o.Id);

        // PublicId
        builder.Property(o => o.PublicId)
               .IsRequired();

        // Appointment Relationship
        builder.HasOne(o => o.Appointment)
               .WithMany(a => a.LaboratoryOrders)
               .HasForeignKey(o => o.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);

        // Laboratory Test Relationship
        builder.HasOne(o => o.LaboratoryTest)
               .WithMany(t => t.LaboratoryOrders)
               .HasForeignKey(o => o.LaboratoryTestId)
               .OnDelete(DeleteBehavior.Restrict);

        // One-to-One Laboratory Result
        builder.HasOne(o => o.LaboratoryResult)
               .WithOne(r => r.LaboratoryOrder)
               .HasForeignKey<LaboratoryResult>(r => r.LaboratoryOrderId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(o => o.AppointmentId);

        builder.HasIndex(o => o.LaboratoryTestId);

        builder.HasIndex(o => o.PublicId)
               .IsUnique();
    }
}