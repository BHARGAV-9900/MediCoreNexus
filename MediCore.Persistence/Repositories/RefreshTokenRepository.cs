using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        RefreshToken token,
        CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(
            token,
            cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .Include(r => r.User)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(
                r => r.Token == token && !r.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .Where(r => r.UserId == userId && !r.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}