using MediatR;

namespace MediCore.Application.Features.Billing.Commands.CreateBill;

public class CreateBillCommand : IRequest<int>
{
    public int AppointmentId { get; set; }

    public decimal TotalAmount { get; set; }
}