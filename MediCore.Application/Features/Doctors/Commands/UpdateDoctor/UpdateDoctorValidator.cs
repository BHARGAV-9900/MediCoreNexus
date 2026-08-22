using FluentValidation;
using System.Text.RegularExpressions;

namespace MediCore.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(BeValidInternationalPhoneNumber)
            .WithMessage("Phone number must be in international format, for example +919876543210 or +14155552671.");

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ConsultationFee)
            .GreaterThan(0);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);
    }

    private static bool BeValidInternationalPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }

        var normalized = phoneNumber.Trim();

        return Regex.IsMatch(normalized, @"^\+\d{8,15}$");
    }
}
