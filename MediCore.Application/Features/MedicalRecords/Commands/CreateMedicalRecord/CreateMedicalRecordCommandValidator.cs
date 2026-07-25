using FluentValidation;

namespace MediCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandValidator
    : AbstractValidator<CreateMedicalRecordCommand>
{
    public CreateMedicalRecordCommandValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0)
            .WithMessage("A valid appointment is required.");

        RuleFor(x => x.Diagnosis)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Symptoms)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.ClinicalNotes)
            .MaximumLength(4000);

        RuleFor(x => x.TreatmentPlan)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(x => x.FollowUpInstructions)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.FollowUpInstructions));
    }
}