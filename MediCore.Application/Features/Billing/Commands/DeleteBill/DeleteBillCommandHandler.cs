using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.DeleteBill;

public class DeleteBillCommandHandler
    : IRequestHandler<DeleteBillCommand, bool>
{
    private readonly IBillRepository _billRepository;

    public DeleteBillCommandHandler(
        IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<bool> Handle(
        DeleteBillCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {request.Id} was not found.");

        bill.Delete();

        await _billRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}
