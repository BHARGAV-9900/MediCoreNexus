using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface ILaboratoryTestRepository
{
    Task AddAsync(
        LaboratoryTest laboratoryTest,
        CancellationToken cancellationToken);

    Task<LaboratoryTest?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<LaboratoryTest>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}