using MediatR;

namespace MediCore.Application.Features.Billing.Commands.CreateBillItem;

public class CreateBillItemCommand : IRequest<int>
{
    public int BillId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}