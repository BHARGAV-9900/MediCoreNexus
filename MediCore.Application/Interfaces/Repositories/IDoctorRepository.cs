using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IDoctorRepository
{
    Task AddAsync(
    Doctor doctor,
    CancellationToken cancellationToken);

    Task<Doctor?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Doctor>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}