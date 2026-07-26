using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IBillRepository
{
    Task AddAsync(
        Bill bill,
        CancellationToken cancellationToken);

    Task<Bill?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Bill>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Bill?> GetByAppointmentIdAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task<bool> ExistsForAppointmentAsync(
        int appointmentId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}