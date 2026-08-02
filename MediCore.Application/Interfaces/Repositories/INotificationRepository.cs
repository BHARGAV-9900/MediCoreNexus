using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken);

    Task<Notification?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Notification>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<Notification>> GetUnreadAsync(
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}