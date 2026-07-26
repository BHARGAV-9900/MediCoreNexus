using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken);

    Task<Payment?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<Payment>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IEnumerable<Payment>> GetByBillIdAsync(
        int billId,
        CancellationToken cancellationToken);

    Task<decimal> GetTotalPaidAmountAsync(
        int billId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}