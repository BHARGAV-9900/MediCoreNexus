using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PublicId)
               .IsRequired();

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(r => r.Name)
               .IsUnique();

        builder.HasIndex(r => r.PublicId)
               .IsUnique();
    }
}