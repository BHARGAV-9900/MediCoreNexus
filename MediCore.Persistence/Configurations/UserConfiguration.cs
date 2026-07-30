using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.PublicId)
               .IsRequired();

        builder.Property(u => u.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.Email)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .IsRequired();

        builder.HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.HasIndex(u => u.PublicId)
               .IsUnique();
    }
}