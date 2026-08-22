using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class BillItemRepository : IBillItemRepository
{
    private readonly ApplicationDbContext _context;

    public BillItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BillItem billItem, CancellationToken cancellationToken)
    {
        await _context.BillItems.AddAsync(billItem, cancellationToken);
    }

    public async Task<BillItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.BillItems
            .Include(x => x.Bill)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<BillItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.BillItems
            .Include(x => x.Bill)
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BillItem>> GetByBillIdAsync(int billId, CancellationToken cancellationToken)
    {
        return await _context.BillItems
            .Where(x => x.BillId == billId && !x.IsDeleted)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}