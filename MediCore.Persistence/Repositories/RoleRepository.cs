using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken)
    {
        await _context.Roles.AddAsync(role, cancellationToken);
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(
                r => r.Id == id && !r.IsDeleted,
                cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(
                r => r.Name == name && !r.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => !r.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AnyAsync(
                r => r.Name == name && !r.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}