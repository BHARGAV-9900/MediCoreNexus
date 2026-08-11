using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Settings.Queries.GetSettings;

public class GetSettingsQueryHandler
    : IRequestHandler<GetSettingsQuery, SettingsDto?>
{
    private readonly ISystemSettingsRepository
        _settingsRepository;

    public GetSettingsQueryHandler(
        ISystemSettingsRepository settingsRepository)
    {
        _settingsRepository =
            settingsRepository;
    }

    public async Task<SettingsDto?> Handle(
        GetSettingsQuery request,
        CancellationToken cancellationToken)
    {
        var settings =
            await _settingsRepository.GetAsync(
                cancellationToken);

        if (settings is null)
            return null;

        return new SettingsDto
        {
            Id = settings.Id,

            HospitalName =
                settings.HospitalName,

            HospitalEmail =
                settings.HospitalEmail,

            HospitalPhone =
                settings.HospitalPhone,

            HospitalAddress =
                settings.HospitalAddress,

            Currency =
                settings.Currency,

            DateFormat =
                settings.DateFormat,

            TimeZone =
                settings.TimeZone,

            DefaultAppointmentDuration =
                settings.DefaultAppointmentDuration,

            LowStockThreshold =
                settings.LowStockThreshold,

            ExpiryWarningDays =
                settings.ExpiryWarningDays,

            EnableNotifications =
                settings.EnableNotifications,

            EnableAppointmentNotifications =
                settings.EnableAppointmentNotifications,

            EnableBillingNotifications =
                settings.EnableBillingNotifications,

            EnableLaboratoryNotifications =
                settings.EnableLaboratoryNotifications
        };
    }
}