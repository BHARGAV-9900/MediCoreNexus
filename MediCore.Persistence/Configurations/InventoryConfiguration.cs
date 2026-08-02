using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BatchNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Supplier)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.StorageLocation)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.QuantityInStock)
            .IsRequired();

        builder.Property(x => x.MinimumStockLevel)
            .IsRequired();

        builder.Property(x => x.ExpiryDate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(x => x.Medicine)
            .WithMany(x => x.Inventories)
            .HasForeignKey(x => x.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}