using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;

namespace MediCore.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;

    public NotificationService(
        INotificationRepository notificationRepository,
        IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public async Task NotifyUserAsync(
        int userId,
        string title,
        string message,
        string type,
        CancellationToken cancellationToken)
    {
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
}
