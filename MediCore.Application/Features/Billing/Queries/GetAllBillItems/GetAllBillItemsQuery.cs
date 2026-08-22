using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetAllBillItems;

public record GetAllBillItemsQuery : IRequest<IEnumerable<BillItemDto>>;