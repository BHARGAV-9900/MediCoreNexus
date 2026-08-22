using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.UpdateBillItem;

public class UpdateBillItemCommandValidator : AbstractValidator<UpdateBillItemCommand>
{
    public UpdateBillItemCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}