using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IInventoryRepository
{
    Task AddAsync(
        Inventory inventory,
        CancellationToken cancellationToken);

    Task<Inventory?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Inventory>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<Inventory>> GetLowStockAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<Inventory>> GetExpiringAsync(
        int days,
        CancellationToken cancellationToken);
    Task<Inventory?> GetByBatchNumberAsync(
        string batchNumber,
        CancellationToken cancellationToken);
    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}