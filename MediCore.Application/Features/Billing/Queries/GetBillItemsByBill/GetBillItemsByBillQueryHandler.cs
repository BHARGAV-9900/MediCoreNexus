using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetBillItemsByBill;

public class GetBillItemsByBillQueryHandler : IRequestHandler<GetBillItemsByBillQuery, IEnumerable<BillItemDto>>
{
    private readonly IBillRepository _billRepository;
    private readonly IBillItemRepository _repository;

    public GetBillItemsByBillQueryHandler(
        IBillRepository billRepository,
        IBillItemRepository repository)
    {
        _billRepository = billRepository;
        _repository = repository;
    }

    public async Task<IEnumerable<BillItemDto>> Handle(
        GetBillItemsByBillQuery request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null)
            throw new NotFoundException($"Bill with Id {request.BillId} was not found.");

        var items = await _repository.GetByBillIdAsync(request.BillId, cancellationToken);

        return items.Select(x => new BillItemDto
        {
            Id = x.Id,
            PublicId = x.PublicId,
            BillId = x.BillId,
            Description = x.Description,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice,
            TotalAmount = x.TotalAmount
        });
    }
}