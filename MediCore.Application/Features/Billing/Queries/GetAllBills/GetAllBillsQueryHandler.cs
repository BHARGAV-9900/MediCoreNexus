using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetAllBills;

public class GetAllBillsQueryHandler
    : IRequestHandler<GetAllBillsQuery, IEnumerable<BillDto>>
{
    private readonly IBillRepository _billRepository;

    public GetAllBillsQueryHandler(
        IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<IEnumerable<BillDto>> Handle(
        GetAllBillsQuery request,
        CancellationToken cancellationToken)
    {
        var bills = await _billRepository.GetAllAsync(
            cancellationToken);

        return bills.Select(b => new BillDto
        {
            Id = b.Id,
            PublicId = b.PublicId,
            AppointmentId = b.AppointmentId,
            AppointmentPublicId = b.Appointment!.PublicId,
            TotalAmount = b.TotalAmount,
            IsPaid = b.IsPaid
        });
    }
}