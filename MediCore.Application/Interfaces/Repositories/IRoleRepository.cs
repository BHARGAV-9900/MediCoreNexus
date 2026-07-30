using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);

    Task AddAsync(Role role, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}