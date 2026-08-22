using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.CreateBillItem;

public class CreateBillItemCommandValidator : AbstractValidator<CreateBillItemCommand>
{
    public CreateBillItemCommandValidator()
    {
        RuleFor(x => x.BillId).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}