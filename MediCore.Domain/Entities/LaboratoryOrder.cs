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
        AppointmentId = appointmentId;
        LaboratoryTestId = laboratoryTestId;
    }

    public int AppointmentId { get; private set; }

    public int LaboratoryTestId { get; private set; }

    public Appointment? Appointment { get; private set; }

    public LaboratoryTest? LaboratoryTest { get; private set; }

    public LaboratoryResult? LaboratoryResult { get; private set; }
}