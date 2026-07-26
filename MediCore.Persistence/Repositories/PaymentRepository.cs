using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(
            payment,
            cancellationToken);
    }

    public async Task<Payment?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Include(p => p.Bill)
            .FirstOrDefaultAsync(
                p => p.Id == id && !p.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Include(p => p.Bill)
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.PaidOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByBillIdAsync(
        int billId,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Include(p => p.Bill)
            .Where(p => p.BillId == billId && !p.IsDeleted)
            .OrderByDescending(p => p.PaidOn)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalPaidAmountAsync(
        int billId,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .Where(p =>
                p.BillId == billId &&
                !p.IsDeleted)
            .SumAsync(
                p => p.Amount,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}