using FluentValidation;

namespace MediCore.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator
    : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0)
            .WithMessage("A valid patient is required.");

        RuleFor(x => x.DoctorId)
            .GreaterThan(0)
            .WithMessage("A valid doctor is required.");

        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(date => date > DateTime.Now)
            .WithMessage("Appointment date must be in the future.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes));
    }
}