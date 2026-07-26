using MediatR;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Billing.Commands.UpdatePayment;

public class UpdatePaymentCommand : IRequest<bool>
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
}