using FluentValidation;

namespace MediCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommandValidator
    : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdatePrescriptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Instructions)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Notes)
            .MaximumLength(2000);
    }
}