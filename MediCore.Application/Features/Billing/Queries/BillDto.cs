namespace MediCore.Application.Features.Billing.Queries;

public class BillDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int AppointmentId { get; set; }

    public Guid AppointmentPublicId { get; set; }

    public decimal TotalAmount { get; set; }

    public bool IsPaid { get; set; }
}