using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task AddAsync(
        Patient patient,
        CancellationToken cancellationToken);

    Task<Patient?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Patient>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}