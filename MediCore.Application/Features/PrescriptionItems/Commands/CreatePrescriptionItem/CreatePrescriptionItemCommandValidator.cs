using FluentValidation;

namespace MediCore.Application.Features.PrescriptionItems.Commands.CreatePrescriptionItem;

public class CreatePrescriptionItemCommandValidator
    : AbstractValidator<CreatePrescriptionItemCommand>
{
    public CreatePrescriptionItemCommandValidator()
    {
        RuleFor(x => x.PrescriptionId)
            .GreaterThan(0);

        RuleFor(x => x.MedicineId)
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