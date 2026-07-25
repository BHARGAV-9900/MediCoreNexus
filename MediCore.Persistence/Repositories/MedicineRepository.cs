using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly ApplicationDbContext _context;

    public MedicineRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Medicine medicine,
        CancellationToken cancellationToken)
    {
        await _context.Medicines.AddAsync(
            medicine,
            cancellationToken);
    }

    public async Task<Medicine?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Medicines
            .FirstOrDefaultAsync(
                m => m.Id == id && !m.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Medicines
            .Where(m => !m.IsDeleted)
            .OrderBy(m => m.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return await _context.Medicines
            .AnyAsync(
                m => m.Name == name && !m.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}