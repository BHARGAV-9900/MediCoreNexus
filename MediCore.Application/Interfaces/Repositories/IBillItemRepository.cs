using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IBillItemRepository
{
    Task AddAsync(BillItem billItem, CancellationToken cancellationToken);
    Task<BillItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<BillItem>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<BillItem>> GetByBillIdAsync(int billId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}