using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Appointment appointment,
        CancellationToken cancellationToken)
    {
        await _context.Appointments.AddAsync(
            appointment,
            cancellationToken);
    }

    public async Task<Appointment?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(
                a => a.Id == id && !a.IsDeleted,
                cancellationToken);
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        CancellationToken cancellationToken)
    {
        return await _context.Appointments.AnyAsync(
            a => a.PatientId == patientId
              && a.DoctorId == doctorId
              && a.AppointmentDate == appointmentDate
              && !a.IsDeleted,
            cancellationToken);
    }

    public async Task<bool> IsDoctorAvailableAsync(
        int doctorId,
        DateTime appointmentDate,
        int? excludeAppointmentId,
        CancellationToken cancellationToken)
    {
        return !await _context.Appointments.AnyAsync(
            a => a.DoctorId == doctorId
              && a.AppointmentDate == appointmentDate
              && a.Status != AppointmentStatus.Cancelled
              && !a.IsDeleted
              && (!excludeAppointmentId.HasValue || a.Id != excludeAppointmentId.Value),
            cancellationToken);
    }

    public async Task<bool> IsPatientAvailableAsync(
        int patientId,
        DateTime appointmentDate,
        int? excludeAppointmentId,
        CancellationToken cancellationToken)
    {
        return !await _context.Appointments.AnyAsync(
            a => a.PatientId == patientId
              && a.AppointmentDate == appointmentDate
              && a.Status != AppointmentStatus.Cancelled
              && !a.IsDeleted
              && (!excludeAppointmentId.HasValue || a.Id != excludeAppointmentId.Value),
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}