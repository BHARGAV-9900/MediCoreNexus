using MediCore.Application.Features.Patients.Queries.GetPagedPatients;
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

    Task<(IEnumerable<Patient> Patients, int TotalCount)> GetPagedAsync(
        GetPagedPatientsQuery request,
        CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<bool> ExistsByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}