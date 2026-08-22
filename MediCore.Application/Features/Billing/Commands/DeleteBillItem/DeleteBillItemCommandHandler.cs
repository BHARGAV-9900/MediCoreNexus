using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.DeleteBillItem;

public class DeleteBillItemCommandHandler : IRequestHandler<DeleteBillItemCommand, bool>
{
    private readonly IBillRepository _billRepository;
    private readonly IBillItemRepository _billItemRepository;

    public DeleteBillItemCommandHandler(
        IBillRepository billRepository,
        IBillItemRepository billItemRepository)
    {
        _billRepository = billRepository;
        _billItemRepository = billItemRepository;
    }

    public async Task<bool> Handle(DeleteBillItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _billItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (item is null)
            throw new NotFoundException($"Bill item with Id {request.Id} was not found.");

        var bill = await _billRepository.GetByIdAsync(item.BillId, cancellationToken);
        if (bill is null)
            throw new NotFoundException($"Bill with Id {item.BillId} was not found.");

        var items = (await _billItemRepository.GetByBillIdAsync(item.BillId, cancellationToken)).ToList();
        if (items.Count == 1)
            throw new ConflictException("A bill must contain at least one active bill item.");

        var remainingTotal = items
            .Where(x => x.Id != item.Id)
            .Sum(x => x.TotalAmount);

        var totalPaid = bill.Payments.Where(x => !x.IsDeleted).Sum(x => x.Amount);

        if (remainingTotal < totalPaid)
            throw new ConflictException(
                $"This item cannot be deleted because the remaining bill total would be less than the amount already paid. Amount paid: {totalPaid:0.00}.");

        item.Delete();
        await _billItemRepository.SaveChangesAsync(cancellationToken);

        bill.Update(remainingTotal);
        bill.UpdatePaymentStatus(totalPaid);
        await _billRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}