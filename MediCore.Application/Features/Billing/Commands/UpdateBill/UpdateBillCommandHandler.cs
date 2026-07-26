using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.UpdateBill;

public class UpdateBillCommandHandler
    : IRequestHandler<UpdateBillCommand, bool>
{
    private readonly IBillRepository _billRepository;

    public UpdateBillCommandHandler(
        IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<bool> Handle(
        UpdateBillCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {request.Id} was not found.");

        bill.Update(request.TotalAmount);

        await _billRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}