using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetBillItemsByBill;

public record GetBillItemsByBillQuery(int BillId) : IRequest<IEnumerable<BillItemDto>>;