using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
            Department department,
            CancellationToken cancellationToken)
    {
        await _context.Departments.AddAsync(
            department,
            cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(
                d => d.Id == id && !d.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Department>> GetAllAsync(
            CancellationToken cancellationToken)
    {
        return await _context.Departments
            .Where(d => !d.IsDeleted)
            .ToListAsync(cancellationToken);
    }
    public async Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AnyAsync(
                d => d.Name == name && !d.IsDeleted,
                cancellationToken);
    }
    public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}