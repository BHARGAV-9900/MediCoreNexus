using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        MedicalRecord medicalRecord,
        CancellationToken cancellationToken)
    {
        await _context.MedicalRecords.AddAsync(
            medicalRecord,
            cancellationToken);
    }

    public async Task<MedicalRecord?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.MedicalRecords
            .Include(m => m.Appointment)
            .FirstOrDefaultAsync(
                m => m.Id == id && !m.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.MedicalRecords
            .Include(m => m.Appointment)
            .Where(m => !m.IsDeleted)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<MedicalRecord?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.MedicalRecords
            .Include(m => m.Appointment)
            .FirstOrDefaultAsync(
                m => m.AppointmentId == appointmentId && !m.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken)
    {
        return await _context.MedicalRecords.AnyAsync(
            m => m.AppointmentId == appointmentId
              && !m.IsDeleted,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}