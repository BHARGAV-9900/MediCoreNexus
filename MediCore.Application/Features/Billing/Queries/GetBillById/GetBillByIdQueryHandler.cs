using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetBillById;

public class GetBillByIdQueryHandler
    : IRequestHandler<GetBillByIdQuery, BillDto>
{
    private readonly IBillRepository _billRepository;

    public GetBillByIdQueryHandler(
        IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<BillDto> Handle(
        GetBillByIdQuery request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {request.Id} was not found.");

        return new BillDto
        {
            Id = bill.Id,
            PublicId = bill.PublicId,
            AppointmentId = bill.AppointmentId,
            AppointmentPublicId = bill.Appointment!.PublicId,
            TotalAmount = bill.TotalAmount,
            IsPaid = bill.IsPaid
        };
    }
}