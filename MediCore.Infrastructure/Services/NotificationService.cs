using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;

namespace MediCore.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISystemSettingsRepository _systemSettingsRepository;

    public NotificationService(
        INotificationRepository notificationRepository,
        IUserRepository userRepository,
        ISystemSettingsRepository systemSettingsRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _systemSettingsRepository = systemSettingsRepository;
    }

    public async Task NotifyUserAsync(
        int userId,
        string title,
        string message,
        string type,
        CancellationToken cancellationToken)
    {
        if (!await IsNotificationEnabledAsync(type, cancellationToken))
            return;

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null || !user.IsActive || user.IsDeleted)
            return;

        var notification = new Notification(userId, title, message, type);

        await _notificationRepository.AddAsync(notification, cancellationToken);
        await _notificationRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task NotifyRolesAsync(
        IEnumerable<UserRole> roles,
        string title,
        string message,
        string type,
        CancellationToken cancellationToken)
    {
        if (!await IsNotificationEnabledAsync(type, cancellationToken))
            return;

        var roleIds = roles.Select(role => (int)role).Distinct().ToHashSet();

        if (roleIds.Count == 0)
            return;

        var users = await _userRepository.GetAllAsync(cancellationToken);

        foreach (var user in users.Where(u =>
                     u.IsActive &&
                     !u.IsDeleted &&
                     roleIds.Contains(u.RoleId)))
        {
            var notification = new Notification(
                user.Id,
                title,
                message,
                type);

            await _notificationRepository.AddAsync(notification, cancellationToken);
        }

        await _notificationRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsNotificationEnabledAsync(
        string type,
        CancellationToken cancellationToken)
    {
        var settings = await _systemSettingsRepository.GetAsync(cancellationToken);

        // Preserve the existing behavior if system settings have not yet
        // been initialized. Once settings exist, they control notification
        // generation centrally in the backend.
        if (settings is null)
            return true;

        // Master switch: when disabled, no normal notification is created.
        if (!settings.EnableNotifications)
            return false;

        return type.Trim().ToLowerInvariant() switch
        {
            "appointment" => settings.EnableAppointmentNotifications,
            "billing" => settings.EnableBillingNotifications,
            "laboratory" => settings.EnableLaboratoryNotifications,
            _ => true
        };
    }
}
