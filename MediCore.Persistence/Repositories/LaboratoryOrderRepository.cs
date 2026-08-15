using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class LaboratoryOrderRepository : ILaboratoryOrderRepository
{
    private readonly ApplicationDbContext _context;

    public LaboratoryOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        LaboratoryOrder laboratoryOrder,
        CancellationToken cancellationToken)
    {
        await _context.LaboratoryOrders.AddAsync(
            laboratoryOrder,
            cancellationToken);
    }


    public async Task<LaboratoryOrder?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryOrders

            .Include(o => o.Appointment)
                .ThenInclude(a => a!.Patient)

            .Include(o => o.Appointment)
                .ThenInclude(a => a!.Doctor)

            .Include(o => o.LaboratoryTest)

            .Include(o => o.LaboratoryResult)

            .FirstOrDefaultAsync(
                o => o.Id == id &&
                     !o.IsDeleted,
                cancellationToken);
    }


    public async Task<IEnumerable<LaboratoryOrder>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryOrders

            .Include(o => o.Appointment)
                .ThenInclude(a => a!.Patient)

            .Include(o => o.Appointment)
                .ThenInclude(a => a!.Doctor)

            .Include(o => o.LaboratoryTest)

            .Include(o => o.LaboratoryResult)

            .Where(o => !o.IsDeleted)

            .OrderByDescending(o => o.CreatedAt)

            .ToListAsync(cancellationToken);
    }


    public async Task<bool> ExistsAsync(
        int appointmentId,
        int laboratoryTestId,
        CancellationToken cancellationToken)
    {
        return await _context.LaboratoryOrders

            .AnyAsync(
                o =>
                    o.AppointmentId == appointmentId &&
                    o.LaboratoryTestId == laboratoryTestId &&
                    !o.IsDeleted,
                cancellationToken);
    }


    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}