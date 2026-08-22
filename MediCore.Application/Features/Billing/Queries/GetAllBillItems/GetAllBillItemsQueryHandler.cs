using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetAllBillItems;

public class GetAllBillItemsQueryHandler : IRequestHandler<GetAllBillItemsQuery, IEnumerable<BillItemDto>>
{
    private readonly IBillItemRepository _repository;

    public GetAllBillItemsQueryHandler(IBillItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BillItemDto>> Handle(
        GetAllBillItemsQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

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