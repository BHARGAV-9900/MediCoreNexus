namespace MediCore.Application.Features.MedicalRecords.Queries;

public class MedicalRecordDto
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public Guid AppointmentPublicId { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public string Symptoms { get; set; } = string.Empty;

    public string ClinicalNotes { get; set; } = string.Empty;

    public string TreatmentPlan { get; set; } = string.Empty;

    public string? FollowUpInstructions { get; set; }
}