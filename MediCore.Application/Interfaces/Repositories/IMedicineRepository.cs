using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IMedicineRepository
{
    Task AddAsync(
        Medicine medicine,
        CancellationToken cancellationToken);

    Task<Medicine?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Medicine>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}