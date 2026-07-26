using FluentValidation;

namespace MediCore.Application.Features.Billing.Commands.CreateBill;

public class CreateBillCommandValidator
    : AbstractValidator<CreateBillCommand>
{
    public CreateBillCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0);

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0);
    }
}