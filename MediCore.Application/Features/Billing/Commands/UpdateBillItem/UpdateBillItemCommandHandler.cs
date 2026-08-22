using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.UpdateBillItem;

public class UpdateBillItemCommandHandler : IRequestHandler<UpdateBillItemCommand, bool>
{
    private readonly IBillRepository _billRepository;
    private readonly IBillItemRepository _billItemRepository;

    public UpdateBillItemCommandHandler(
        IBillRepository billRepository,
        IBillItemRepository billItemRepository)
    {
        _billRepository = billRepository;
        _billItemRepository = billItemRepository;
    }

    public async Task<bool> Handle(UpdateBillItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _billItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (item is null)
            throw new NotFoundException($"Bill item with Id {request.Id} was not found.");

        var bill = await _billRepository.GetByIdAsync(item.BillId, cancellationToken);
        if (bill is null)
            throw new NotFoundException($"Bill with Id {item.BillId} was not found.");

        var items = await _billItemRepository.GetByBillIdAsync(item.BillId, cancellationToken);
        var otherItemsTotal = items
            .Where(x => x.Id != item.Id)
            .Sum(x => x.TotalAmount);
        var newTotal = otherItemsTotal + (request.Quantity * request.UnitPrice);
        var totalPaid = bill.Payments.Where(x => !x.IsDeleted).Sum(x => x.Amount);

        if (newTotal < totalPaid)
            throw new ConflictException(
                $"Bill total cannot be less than the amount already paid. Amount paid: {totalPaid:0.00}.");

        item.Update(request.Description, request.Quantity, request.UnitPrice);
        await _billItemRepository.SaveChangesAsync(cancellationToken);

        bill.Update(newTotal);
        bill.UpdatePaymentStatus(totalPaid);
        await _billRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}