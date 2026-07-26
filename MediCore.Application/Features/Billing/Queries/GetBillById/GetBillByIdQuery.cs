using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetBillById;

public record GetBillByIdQuery(int Id)
    : IRequest<BillDto>;