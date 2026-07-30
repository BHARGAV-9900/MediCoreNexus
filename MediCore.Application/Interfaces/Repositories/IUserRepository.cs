using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}