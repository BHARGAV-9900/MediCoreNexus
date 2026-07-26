using MediatR;

namespace MediCore.Application.Features.Billing.Commands.UpdateBill;

public class UpdateBillCommand : IRequest<bool>
{
    public int Id { get; set; }

    public decimal TotalAmount { get; set; }
}