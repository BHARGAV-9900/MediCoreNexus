using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class LaboratoryTestRepository : ILaboratoryTestRepository
{
    private readonly ApplicationDbContext _context;

    public LaboratoryTestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        LaboratoryTest laboratoryTest,
        CancellationToken cancellationToken)
    {
        await _context.LaboratoryTests.AddAsync(
            laboratoryTest,
            cancellationToken);
    }

    public async Task<LaboratoryTest?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryTests
            .FirstOrDefaultAsync(
                t => t.Id == id && !t.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<LaboratoryTest>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryTests
            .Where(t => !t.IsDeleted)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
         string name,
         int excludeId,
         CancellationToken cancellationToken)
    {
        return await _context.LaboratoryTests
            .AnyAsync(
                t =>
                    t.Name == name &&
                    t.Id != excludeId &&
                    !t.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}