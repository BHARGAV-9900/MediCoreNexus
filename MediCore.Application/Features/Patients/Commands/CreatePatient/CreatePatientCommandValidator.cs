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

        // Phone numbers are stored in international E.164-style format.
        // Example: +919999999999 or +14155552671
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches("^\\+[1-9]\\d{7,14}$")
            .WithMessage("Phone number must include a country code and contain 8 to 15 digits after the + sign.");

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
            .Matches("^\\+[1-9]\\d{7,14}$")
            .WithMessage("Emergency contact phone must include a country code and contain 8 to 15 digits after the + sign.");
    }
}
