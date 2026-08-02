using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Inventory inventory,
        CancellationToken cancellationToken)
    {
        await _context.Inventories.AddAsync(
            inventory,
            cancellationToken);
    }

    public async Task<Inventory?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .Include(x => x.Medicine)
            .FirstOrDefaultAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .Include(x => x.Medicine)
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Medicine!.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetLowStockAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .Include(x => x.Medicine)
            .Where(x =>
                !x.IsDeleted &&
                x.QuantityInStock <= x.MinimumStockLevel)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Inventory>> GetExpiringAsync(
        int days,
        CancellationToken cancellationToken)
    {
        var expiryDate = DateTime.UtcNow.AddDays(days);

        return await _context.Inventories
            .Include(x => x.Medicine)
            .Where(x =>
                !x.IsDeleted &&
                x.ExpiryDate <= expiryDate)
            .ToListAsync(cancellationToken);
    }
    public async Task<Inventory?> GetByBatchNumberAsync(
        string batchNumber,
        CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .Include(x => x.Medicine)
            .FirstOrDefaultAsync(
                x => x.BatchNumber == batchNumber &&
                     !x.IsDeleted,
                cancellationToken);
    }
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}