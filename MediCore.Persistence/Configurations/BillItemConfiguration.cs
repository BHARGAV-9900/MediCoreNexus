using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class BillItemConfiguration : IEntityTypeConfiguration<BillItem>
{
    public void Configure(EntityTypeBuilder<BillItem> builder)
    {
        builder.ToTable("BillItems");
        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.PublicId)
               .IsRequired();

        builder.Property(bi => bi.Description)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(bi => bi.Quantity)
               .IsRequired();

        builder.Property(bi => bi.UnitPrice)
               .IsRequired()
               .HasPrecision(18, 2);

        builder.Ignore(bi => bi.TotalAmount);

        builder.HasOne(bi => bi.Bill)
               .WithMany(b => b.BillItems)
               .HasForeignKey(bi => bi.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(bi => bi.BillId);
        builder.HasIndex(bi => bi.PublicId).IsUnique();
    }
}