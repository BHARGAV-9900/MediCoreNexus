using FluentValidation;

namespace MediCore.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidator
    : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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
            .Matches("^\\d{10,15}$")
            .WithMessage("Phone number must contain 10 to 15 digits.");

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
            .Matches("^\\d{10,15}$")
            .WithMessage("Emergency contact phone must contain 10 to 15 digits.");
    }
}