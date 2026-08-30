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
        _settingsRepository = settingsRepository;
    }

    public async Task<SettingsDto?> Handle(
        GetSettingsQuery request,
        CancellationToken cancellationToken)
    {
        var settings = await _settingsRepository.GetAsync(cancellationToken);

        if (settings is null)
            return null;

        // Existing databases may contain the previous display format.
        // Return the new supported format so the UI never receives a value
        // that is no longer present in the Settings dropdown.
        var dateFormat = settings.DateFormat == "dd-MMM-yyyy"
            ? "dd/MM/yyyy"
            : settings.DateFormat;

        return new SettingsDto
        {
            Id = settings.Id,
            HospitalName = settings.HospitalName,
            HospitalEmail = settings.HospitalEmail,
            HospitalPhone = settings.HospitalPhone,
            HospitalAddress = settings.HospitalAddress,
            Currency = settings.Currency,
            DateFormat = dateFormat,
            TimeZone = settings.TimeZone,
            DefaultAppointmentDuration = settings.DefaultAppointmentDuration,
            LowStockThreshold = settings.LowStockThreshold,
            ExpiryWarningDays = settings.ExpiryWarningDays,
            EnableNotifications = settings.EnableNotifications,
            EnableAppointmentNotifications = settings.EnableAppointmentNotifications,
            EnableBillingNotifications = settings.EnableBillingNotifications,
            EnableLaboratoryNotifications = settings.EnableLaboratoryNotifications
        };
    }
}