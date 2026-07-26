using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.CreatePayment;

public class CreatePaymentCommandValidator
    : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.BillId)
            .GreaterThan(0);

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum();
    }
}