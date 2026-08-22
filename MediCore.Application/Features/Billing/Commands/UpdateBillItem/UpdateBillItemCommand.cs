using MediatR;

namespace MediCore.Application.Features.Billing.Commands.UpdateBillItem;

public class UpdateBillItemCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}