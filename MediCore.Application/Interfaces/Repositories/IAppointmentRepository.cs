using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task AddAsync(
        Appointment appointment,
        CancellationToken cancellationToken);

    Task<Appointment?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Appointment>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        CancellationToken cancellationToken);

    Task<bool> IsDoctorAvailableAsync(
        int doctorId,
        DateTime appointmentDate,
        int? excludeAppointmentId,
        CancellationToken cancellationToken);

    Task<bool> IsPatientAvailableAsync(
        int patientId,
        DateTime appointmentDate,
        int? excludeAppointmentId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}