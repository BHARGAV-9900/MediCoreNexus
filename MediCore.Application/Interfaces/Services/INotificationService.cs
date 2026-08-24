using MediCore.Domain.Enums;

namespace MediCore.Application.Interfaces.Services;

public interface INotificationService
{
    Task NotifyUserAsync(
        int userId,
        string title,
        string message,
        string type,
        CancellationToken cancellationToken);

    Task NotifyRolesAsync(
        IEnumerable<UserRole> roles,
        string title,
        string message,
        string type,
        CancellationToken cancellationToken);
}
