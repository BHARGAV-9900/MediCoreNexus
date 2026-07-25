using MediatR;

namespace MediCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommand : IRequest<int>
{
    public int AppointmentId { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public string Symptoms { get; set; } = string.Empty;

    public string ClinicalNotes { get; set; } = string.Empty;

    public string TreatmentPlan { get; set; } = string.Empty;

    public string? FollowUpInstructions { get; set; }
}