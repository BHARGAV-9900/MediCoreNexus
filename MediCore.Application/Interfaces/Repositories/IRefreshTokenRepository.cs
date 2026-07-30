using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken);

    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);

    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}