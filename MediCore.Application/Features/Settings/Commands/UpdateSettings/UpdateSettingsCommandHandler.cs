using MediatR;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Settings.Commands.UpdateSettings;

public class UpdateSettingsCommandHandler
    : IRequestHandler<UpdateSettingsCommand, bool>
{
    private readonly ISystemSettingsRepository
        _settingsRepository;

    public UpdateSettingsCommandHandler(
        ISystemSettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    public async Task<bool> Handle(
        UpdateSettingsCommand request,
        CancellationToken cancellationToken)
    {
        var settings =
            await _settingsRepository.GetForUpdateAsync(
                cancellationToken);

        if (settings is null)
        {
            settings = new SystemSettings(
                request.HospitalName,
                request.HospitalEmail,
                request.HospitalPhone,
                request.HospitalAddress,
                request.Currency,
                request.DateFormat,
                request.TimeZone,
                request.DefaultAppointmentDuration,
                request.LowStockThreshold,
                request.ExpiryWarningDays);

            settings.Update(
                request.HospitalName,
                request.HospitalEmail,
                request.HospitalPhone,
                request.HospitalAddress,
                request.Currency,
                request.DateFormat,
                request.TimeZone,
                request.DefaultAppointmentDuration,
                request.LowStockThreshold,
                request.ExpiryWarningDays,
                request.EnableNotifications,
                request.EnableAppointmentNotifications,
                request.EnableBillingNotifications,
                request.EnableLaboratoryNotifications);

            await _settingsRepository.AddAsync(
                settings,
                cancellationToken);
        }
        else
        {
            settings.Update(
                request.HospitalName,
                request.HospitalEmail,
                request.HospitalPhone,
                request.HospitalAddress,
                request.Currency,
                request.DateFormat,
                request.TimeZone,
                request.DefaultAppointmentDuration,
                request.LowStockThreshold,
                request.ExpiryWarningDays,
                request.EnableNotifications,
                request.EnableAppointmentNotifications,
                request.EnableBillingNotifications,
                request.EnableLaboratoryNotifications);
        }

        await _settingsRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}