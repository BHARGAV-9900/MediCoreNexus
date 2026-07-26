using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IPrescriptionRepository
{
    Task AddAsync(
        Prescription prescription,
        CancellationToken cancellationToken);

    Task<Prescription?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Prescription>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Prescription?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}