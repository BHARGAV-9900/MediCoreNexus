using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.UpdatePayment;

public class UpdatePaymentCommandValidator
    : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum();
    }
}