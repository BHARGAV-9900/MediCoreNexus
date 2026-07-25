using FluentValidation;

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
            .MaximumLength(20);

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
}