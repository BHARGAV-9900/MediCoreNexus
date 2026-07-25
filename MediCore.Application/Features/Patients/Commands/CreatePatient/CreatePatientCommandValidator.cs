using FluentValidation;

namespace MediCore.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator
    : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today)
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.EmergencyContactName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.EmergencyContactPhone)
            .NotEmpty()
            .MaximumLength(15);
    }
}