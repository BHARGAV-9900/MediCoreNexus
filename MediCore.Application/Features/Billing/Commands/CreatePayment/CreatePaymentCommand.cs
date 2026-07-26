using MediatR;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Billing.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<int>
{
    public int BillId { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
}