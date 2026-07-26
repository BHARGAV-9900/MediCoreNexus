using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class PrescriptionItemRepository : IPrescriptionItemRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        PrescriptionItem prescriptionItem,
        CancellationToken cancellationToken)
    {
        await _context.PrescriptionItems.AddAsync(
            prescriptionItem,
            cancellationToken);
    }

    public async Task<PrescriptionItem?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.PrescriptionItems
            .Include(pi => pi.Prescription)
            .Include(pi => pi.Medicine)
            .FirstOrDefaultAsync(
                pi => pi.Id == id && !pi.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<PrescriptionItem>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.PrescriptionItems
            .Include(pi => pi.Prescription)
            .Include(pi => pi.Medicine)
            .Where(pi => !pi.IsDeleted)
            .OrderByDescending(pi => pi.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PrescriptionItem>> GetByPrescriptionIdAsync(
        int prescriptionId,
        CancellationToken cancellationToken)
    {
        return await _context.PrescriptionItems
            .Include(pi => pi.Prescription)
            .Include(pi => pi.Medicine)
            .Where(pi =>
                pi.PrescriptionId == prescriptionId &&
                !pi.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        int prescriptionId,
        int medicineId,
        CancellationToken cancellationToken)
    {
        return await _context.PrescriptionItems
            .AnyAsync(pi =>
                pi.PrescriptionId == prescriptionId &&
                pi.MedicineId == medicineId &&
                !pi.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}