using MediatR;

namespace MediCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommand : IRequest
{
    public int Id { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public string Symptoms { get; set; } = string.Empty;

    public string ClinicalNotes { get; set; } = string.Empty;

    public string TreatmentPlan { get; set; } = string.Empty;

    public string? FollowUpInstructions { get; set; }
}