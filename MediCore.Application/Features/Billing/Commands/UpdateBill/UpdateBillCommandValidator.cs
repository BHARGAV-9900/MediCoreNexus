using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.UpdateBill;

public class UpdateBillCommandValidator
    : AbstractValidator<UpdateBillCommand>
{
    public UpdateBillCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0);
    }
}