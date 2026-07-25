using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task AddAsync(
    Department department,
    CancellationToken cancellationToken);

    Task<Department?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Department>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        string name,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}