using FluentValidation;

namespace MediCore.Application.Features.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandValidator
    : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0);

        RuleFor(x => x.Instructions)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.Notes)
            .MaximumLength(2000);
    }
}