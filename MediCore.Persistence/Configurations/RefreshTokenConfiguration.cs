using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PublicId)
               .IsRequired();

        builder.Property(r => r.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(r => r.ExpiresOn)
               .IsRequired();

        builder.Property(r => r.IsRevoked)
               .IsRequired();

        builder.HasOne(r => r.User)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.Token)
               .IsUnique();

        builder.HasIndex(r => r.PublicId)
               .IsUnique();
    }
}