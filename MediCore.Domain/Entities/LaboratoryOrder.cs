using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class LaboratoryOrder : BaseAuditableEntity
{
    private LaboratoryOrder()
    {
    }

    public LaboratoryOrder(
        int appointmentId,
        int laboratoryTestId)
    {
        SetAppointment(appointmentId);
        SetLaboratoryTest(laboratoryTestId);
    }

    public int AppointmentId { get; private set; }

    public int LaboratoryTestId { get; private set; }

    public Appointment? Appointment { get; private set; }

    public LaboratoryTest? LaboratoryTest { get; private set; }

    public LaboratoryResult? LaboratoryResult { get; private set; }

    private void SetAppointment(int appointmentId)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Invalid appointment.");

        AppointmentId = appointmentId;
    }
    private void SetLaboratoryTest(int laboratoryTestId)
    {
        if (laboratoryTestId <= 0)
            throw new ArgumentException("Invalid laboratory test.");

        LaboratoryTestId = laboratoryTestId;
    }
    public void Update(
        int appointmentId,
        int laboratoryTestId)
    {
        SetAppointment(appointmentId);
        SetLaboratoryTest(laboratoryTestId);

        UpdatedAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}