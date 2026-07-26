using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface ILaboratoryOrderRepository
{
    Task AddAsync(
        LaboratoryOrder laboratoryOrder,
        CancellationToken cancellationToken);

    Task<LaboratoryOrder?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<LaboratoryOrder>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int appointmentId,
        int laboratoryTestId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}