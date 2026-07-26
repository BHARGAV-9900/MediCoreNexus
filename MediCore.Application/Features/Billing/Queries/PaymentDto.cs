using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Billing.Queries;

public class PaymentDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int BillId { get; set; }

    public Guid BillPublicId { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime PaidOn { get; set; }
}