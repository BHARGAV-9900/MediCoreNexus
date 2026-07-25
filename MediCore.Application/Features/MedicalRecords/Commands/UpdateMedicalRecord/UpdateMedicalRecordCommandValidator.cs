using FluentValidation;

namespace MediCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommandValidator
    : AbstractValidator<UpdateMedicalRecordCommand>
{
    public UpdateMedicalRecordCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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