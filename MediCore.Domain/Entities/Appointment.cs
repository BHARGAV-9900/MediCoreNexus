using MediCore.Domain.Common;
using MediCore.Domain.Enums;

namespace MediCore.Domain.Entities;

public class Appointment : BaseAuditableEntity
{
    // Required by EF Core
    private Appointment()
    {
    }

    // Business Constructor
    public Appointment(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        string reason,
        string? notes = null)
    {
        SetPatient(patientId);
        SetDoctor(doctorId);
        SetAppointmentDate(appointmentDate);
        SetReason(reason);

        Notes = notes;

        Status = AppointmentStatus.Scheduled;
    }

    // Foreign Keys

    public int PatientId { get; private set; }

    public int DoctorId { get; private set; }

    // Appointment Details

    public DateTime AppointmentDate { get; private set; }

    public AppointmentStatus Status { get; private set; }

    public string Reason { get; private set; } = string.Empty;

    public string? Notes { get; private set; }

    // Navigation Properties

    public Patient? Patient { get; private set; }

    public Doctor? Doctor { get; private set; }

    public MedicalRecord? MedicalRecord { get; private set; }
    public Prescription? Prescription { get; private set; }
    public Bill? Bill { get; private set; }
    public ICollection<LaboratoryOrder> LaboratoryOrders { get; private set; }
    = new List<LaboratoryOrder>();
    //Domain Behaviors
    public void Complete()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new ArgumentException("Cancelled appointments cannot be completed.");

        Status = AppointmentStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new ArgumentException("Completed appointments cannot be cancelled.");

        Status = AppointmentStatus.Cancelled;
    }

    public void CheckIn()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new ArgumentException("Only scheduled appointments can be checked in.");

        Status = AppointmentStatus.CheckedIn;
    }

    public void StartConsultation()
    {
        if (Status != AppointmentStatus.CheckedIn)
            throw new ArgumentException("Patient must check in first.");

        Status = AppointmentStatus.InProgress;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }

    //Validation Methods
    private void SetPatient(int patientId)
    {
        if (patientId <= 0)
            throw new ArgumentException("Invalid patient.");

        PatientId = patientId;
    }

    private void SetDoctor(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException("Invalid doctor.");

        DoctorId = doctorId;
    }

    private void SetAppointmentDate(DateTime appointmentDate)
    {
        if (appointmentDate < DateTime.Now)
            throw new ArgumentException("Appointment cannot be scheduled in the past.");

        AppointmentDate = appointmentDate;
    }

    private void SetReason(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason is required.");

        Reason = reason.Trim();
    }
}