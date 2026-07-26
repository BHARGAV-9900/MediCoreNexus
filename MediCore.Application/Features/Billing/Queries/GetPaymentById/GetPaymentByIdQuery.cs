using MediatR;

namespace MediCore.Application.Features.Billing.Queries.GetPaymentById;

public record GetPaymentByIdQuery(int Id)
    : IRequest<PaymentDto>;