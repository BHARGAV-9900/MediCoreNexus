using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetAllPayments;

public record GetAllPaymentsQuery
    : IRequest<IEnumerable<PaymentDto>>;