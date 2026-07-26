using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class BillRepository : IBillRepository
{
    private readonly ApplicationDbContext _context;

    public BillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Bill bill,
        CancellationToken cancellationToken)
    {
        await _context.Bills.AddAsync(
            bill,
            cancellationToken);
    }

    public async Task<Bill?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Bills
            .Include(b => b.Appointment)
            .Include(b => b.Payments)
            .FirstOrDefaultAsync(
                b => b.Id == id && !b.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Bill>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Bills
            .Include(b => b.Appointment)
            .Include(b => b.Payments)
            .Where(b => !b.IsDeleted)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Bill?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.Bills
            .Include(b => b.Payments)
            .FirstOrDefaultAsync(
                b => b.AppointmentId == appointmentId &&
                     !b.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.Bills
            .AnyAsync(
                b => b.AppointmentId == appointmentId &&
                     !b.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}