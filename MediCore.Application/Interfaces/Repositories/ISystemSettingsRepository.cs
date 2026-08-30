using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface ISystemSettingsRepository
{
    Task<SystemSettings?> GetAsync(
        CancellationToken cancellationToken);

    Task<SystemSettings?> GetForUpdateAsync(
        CancellationToken cancellationToken);

    Task AddAsync(
        SystemSettings settings,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}