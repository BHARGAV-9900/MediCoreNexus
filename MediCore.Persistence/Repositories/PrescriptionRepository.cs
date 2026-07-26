using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Prescription prescription,
        CancellationToken cancellationToken)
    {
        await _context.Prescriptions.AddAsync(
            prescription,
            cancellationToken);
    }

    public async Task<Prescription?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.Appointment)
            .Include(p => p.PrescriptionItems)
            .FirstOrDefaultAsync(
                p => p.Id == id && !p.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Prescription>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.Appointment)
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Prescription?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.PrescriptionItems)
            .FirstOrDefaultAsync(
                p => p.AppointmentId == appointmentId && !p.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .AnyAsync(
                p => p.AppointmentId == appointmentId && !p.IsDeleted,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}