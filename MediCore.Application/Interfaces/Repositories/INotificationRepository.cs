using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken);

    Task<Notification?> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken);

    Task<IEnumerable<Notification>> GetAllAsync(
        int userId,
        CancellationToken cancellationToken);

    Task<IEnumerable<Notification>> GetUnreadAsync(
        int userId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}