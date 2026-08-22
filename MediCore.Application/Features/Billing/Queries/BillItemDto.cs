namespace MediCore.Application.Features.Billing.Queries;

public class BillItemDto
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public int BillId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
}