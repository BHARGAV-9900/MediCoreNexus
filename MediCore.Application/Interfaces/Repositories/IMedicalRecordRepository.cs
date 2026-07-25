using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IMedicalRecordRepository
{
    Task AddAsync(
        MedicalRecord medicalRecord,
        CancellationToken cancellationToken);

    Task<MedicalRecord?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<MedicalRecord>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<MedicalRecord?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}