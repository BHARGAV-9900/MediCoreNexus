using MediatR;

namespace MediCore.Application.Features.Billing.Commands.DeletePayment;

public record DeletePaymentCommand(int Id) : IRequest<bool>;