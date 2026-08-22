using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IPrescriptionItemRepository
{
    Task AddAsync(
        PrescriptionItem prescriptionItem,
        CancellationToken cancellationToken);

    Task<PrescriptionItem?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<PrescriptionItem>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<PrescriptionItem>> GetByPrescriptionIdAsync(
        int prescriptionId,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        int prescriptionId,
        int medicineId,
        CancellationToken cancellationToken,
        int? excludeId = null);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}