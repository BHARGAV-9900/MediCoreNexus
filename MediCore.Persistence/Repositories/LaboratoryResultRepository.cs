using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class LaboratoryResultRepository : ILaboratoryResultRepository
{
    private readonly ApplicationDbContext _context;

    public LaboratoryResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        LaboratoryResult laboratoryResult,
        CancellationToken cancellationToken)
    {
        await _context.LaboratoryResults.AddAsync(
            laboratoryResult,
            cancellationToken);
    }

    public async Task<LaboratoryResult?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryResults
            .Include(r => r.LaboratoryOrder)
            .FirstOrDefaultAsync(
                r => r.Id == id && !r.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<LaboratoryResult>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryResults
            .Include(r => r.LaboratoryOrder)
            .Where(r => !r.IsDeleted)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByLaboratoryOrderAsync(
        int laboratoryOrderId,
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryResults
            .AnyAsync(
                r => r.LaboratoryOrderId == laboratoryOrderId &&
                     !r.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}