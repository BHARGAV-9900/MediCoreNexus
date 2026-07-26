using FluentValidation;

namespace MediCore.Application.Features.PrescriptionItems.Commands.UpdatePrescriptionItem;

public class UpdatePrescriptionItemCommandValidator
    : AbstractValidator<UpdatePrescriptionItemCommand>
{
    public UpdatePrescriptionItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Dosage)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Frequency)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DurationInDays)
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}