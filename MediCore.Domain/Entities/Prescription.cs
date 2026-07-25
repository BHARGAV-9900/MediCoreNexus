using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Prescription : BaseAuditableEntity
{
    private Prescription()
    {
    }

    public Prescription(
        int appointmentId,
        string instructions,
        string? notes = null)
    {
        SetAppointment(appointmentId);
        SetInstructions(instructions);

        Notes = notes;
    }

    public int AppointmentId { get; private set; }

    public string Instructions { get; private set; } = string.Empty;

    public string? Notes { get; private set; }

    // Navigation Property
    public Appointment? Appointment { get; private set; }

    // Navigation Property
    public ICollection<PrescriptionItem> PrescriptionItems { get; private set; }
        = new List<PrescriptionItem>();

    public void UpdateInstructions(string instructions)
    {
        SetInstructions(instructions);
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }

    private void SetAppointment(int appointmentId)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Invalid appointment.");

        AppointmentId = appointmentId;
    }

    private void SetInstructions(string instructions)
    {
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Instructions are required.");

        Instructions = instructions.Trim();
    }
}