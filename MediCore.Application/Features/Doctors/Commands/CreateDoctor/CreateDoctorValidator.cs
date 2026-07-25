using FluentValidation;

namespace MediCore.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .WithMessage("Specialization is required.")
            .MaximumLength(100);

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ConsultationFee)
            .GreaterThan(0);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(150);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(20);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0)
            .WithMessage("Department is required.");
    }
}