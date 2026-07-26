using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Bill : BaseAuditableEntity
{
    private Bill()
    {
    }

    public Bill(
        int appointmentId,
        decimal totalAmount)
    {
        SetAppointment(appointmentId);
        SetTotalAmount(totalAmount);

        IsPaid = false;
    }

    public int AppointmentId { get; private set; }

    public decimal TotalAmount { get; private set; }

    public bool IsPaid { get; private set; }

    public Appointment? Appointment { get; private set; }

    public ICollection<Payment> Payments { get; private set; }
        = new List<Payment>();

    public void MarkAsPaid()
    {
        if (IsPaid)
            return;

        IsPaid = true;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetAppointment(int appointmentId)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Invalid appointment.");

        AppointmentId = appointmentId;
    }

    private void SetTotalAmount(decimal totalAmount)
    {
        if (totalAmount <= 0)
            throw new ArgumentException("Total amount must be greater than zero.");

        TotalAmount = totalAmount;
    }
    public void Update(decimal totalAmount)
    {
        SetTotalAmount(totalAmount);

        UpdatedAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}