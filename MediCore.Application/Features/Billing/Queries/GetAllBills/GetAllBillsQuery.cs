using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetAllBills;

public record GetAllBillsQuery
    : IRequest<IEnumerable<BillDto>>;