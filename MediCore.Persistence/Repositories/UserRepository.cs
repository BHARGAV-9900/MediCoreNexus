using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(
                u => u.Id == id && !u.IsDeleted,
                cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(
                u => u.Email == email && !u.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(
                u => u.Email == email && !u.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}