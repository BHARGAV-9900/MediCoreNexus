using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class MedicalRecord : BaseAuditableEntity
{
    private MedicalRecord()
    {
    }

    public MedicalRecord(
        int appointmentId,
        string diagnosis,
        string symptoms,
        string clinicalNotes,
        string treatmentPlan,
        string? followUpInstructions = null)
    {
        SetAppointment(appointmentId);
        SetDiagnosis(diagnosis);

        Symptoms = symptoms;
        ClinicalNotes = clinicalNotes;
        TreatmentPlan = treatmentPlan;
        FollowUpInstructions = followUpInstructions;
    }

    public int AppointmentId { get; private set; }

    public string Diagnosis { get; private set; } = string.Empty;

    public string Symptoms { get; private set; } = string.Empty;

    public string ClinicalNotes { get; private set; } = string.Empty;

    public string TreatmentPlan { get; private set; } = string.Empty;

    public string? FollowUpInstructions { get; private set; }

    // Navigation Property
    public Appointment? Appointment { get; private set; }

    public void UpdateTreatmentPlan(string treatmentPlan)
    {
        TreatmentPlan = treatmentPlan;
    }

    public void UpdateClinicalNotes(string notes)
    {
        ClinicalNotes = notes;
    }

    private void SetAppointment(int appointmentId)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Invalid appointment.");

        AppointmentId = appointmentId;
    }

    private void SetDiagnosis(string diagnosis)
    {
        if (string.IsNullOrWhiteSpace(diagnosis))
            throw new ArgumentException("Diagnosis is required.");

        Diagnosis = diagnosis.Trim();
    }
}