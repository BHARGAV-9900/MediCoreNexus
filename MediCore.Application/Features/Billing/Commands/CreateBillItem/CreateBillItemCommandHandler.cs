using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Billing.Commands.CreateBillItem;

public class CreateBillItemCommandHandler : IRequestHandler<CreateBillItemCommand, int>
{
    private readonly IBillRepository _billRepository;
    private readonly IBillItemRepository _billItemRepository;

    public CreateBillItemCommandHandler(
        IBillRepository billRepository,
        IBillItemRepository billItemRepository)
    {
        _billRepository = billRepository;
        _billItemRepository = billItemRepository;
    }

    public async Task<int> Handle(CreateBillItemCommand request, CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null)
            throw new NotFoundException($"Bill with Id {request.BillId} was not found.");

        var existingItems = await _billItemRepository.GetByBillIdAsync(request.BillId, cancellationToken);
        var newTotal = existingItems.Sum(x => x.TotalAmount) + (request.Quantity * request.UnitPrice);
        var totalPaid = bill.Payments.Where(x => !x.IsDeleted).Sum(x => x.Amount);

        if (newTotal < totalPaid)
            throw new ConflictException(
                $"Bill total cannot be less than the amount already paid. Amount paid: {totalPaid:0.00}.");

        var item = new BillItem(
            request.BillId,
            request.Description,
            request.Quantity,
            request.UnitPrice);

        await _billItemRepository.AddAsync(item, cancellationToken);
        await _billItemRepository.SaveChangesAsync(cancellationToken);

        bill.Update(newTotal);
        bill.UpdatePaymentStatus(totalPaid);
        await _billRepository.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}