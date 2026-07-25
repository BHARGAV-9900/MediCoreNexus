using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class LaboratoryResultConfiguration : IEntityTypeConfiguration<LaboratoryResult>
{
    public void Configure(EntityTypeBuilder<LaboratoryResult> builder)
    {
        // Table
        builder.ToTable("LaboratoryResults");

        // Primary Key
        builder.HasKey(r => r.Id);

        // PublicId
        builder.Property(r => r.PublicId)
               .IsRequired();

        // Result
        builder.Property(r => r.Result)
               .IsRequired()
               .HasMaxLength(4000);

        // Remarks
        builder.Property(r => r.Remarks)
               .HasMaxLength(2000);

        // One-to-One Relationship
        builder.HasOne(r => r.LaboratoryOrder)
               .WithOne(o => o.LaboratoryResult)
               .HasForeignKey<LaboratoryResult>(r => r.LaboratoryOrderId)
               .OnDelete(DeleteBehavior.Cascade);

        // Unique Index
        builder.HasIndex(r => r.LaboratoryOrderId)
               .IsUnique();

        builder.HasIndex(r => r.PublicId)
               .IsUnique();
    }
}