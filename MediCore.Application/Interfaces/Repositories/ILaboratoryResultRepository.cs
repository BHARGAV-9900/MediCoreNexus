using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface ILaboratoryResultRepository
{
    Task AddAsync(
        LaboratoryResult laboratoryResult,
        CancellationToken cancellationToken);

    Task<LaboratoryResult?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<LaboratoryResult>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByLaboratoryOrderAsync(
        int laboratoryOrderId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}