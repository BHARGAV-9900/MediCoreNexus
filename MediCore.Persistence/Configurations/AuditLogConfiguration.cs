using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired(false);

        builder.Property(x => x.UserEmail)
            .HasMaxLength(320)
            .IsRequired(false);

        builder.Property(x => x.Role)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Action)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.EntityName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.EntityPublicId)
            .HasMaxLength(36)
            .IsRequired(false);

        builder.Property(x => x.OldValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.NewValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(64)
            .IsRequired(false);

        builder.Property(x => x.RequestPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.RequestId)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.OccurredAtUtc)
            .IsRequired();

        builder.HasIndex(x => x.OccurredAtUtc);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntityName);
        builder.HasIndex(x => x.Action);
    }
}
