using MediCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCore.Persistence.Configurations;

public class SystemSettingsConfiguration
    : IEntityTypeConfiguration<SystemSettings>
{
    public void Configure(
        EntityTypeBuilder<SystemSettings> builder)
    {
        builder.ToTable("SystemSettings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.HospitalName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.HospitalEmail)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.HospitalPhone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.HospitalAddress)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.DateFormat)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.TimeZone)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DefaultAppointmentDuration)
            .IsRequired();

        builder.Property(x => x.LowStockThreshold)
            .IsRequired();

        builder.Property(x => x.ExpiryWarningDays)
            .IsRequired();

        builder.Property(x => x.EnableNotifications)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.EnableAppointmentNotifications)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.EnableBillingNotifications)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.EnableLaboratoryNotifications)
            .IsRequired()
            .HasDefaultValue(true);
    }
}