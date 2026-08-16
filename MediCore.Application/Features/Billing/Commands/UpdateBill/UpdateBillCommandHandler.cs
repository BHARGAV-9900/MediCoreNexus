using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.UpdateBill;

public class UpdateBillCommandHandler
    : IRequestHandler<UpdateBillCommand, bool>
{
    private readonly IBillRepository _billRepository;
    private readonly IPaymentRepository _paymentRepository;


    public UpdateBillCommandHandler(
        IBillRepository billRepository,
        IPaymentRepository paymentRepository)
    {
        _billRepository = billRepository;
        _paymentRepository = paymentRepository;
    }


    public async Task<bool> Handle(
        UpdateBillCommand request,
        CancellationToken cancellationToken)
    {
        var bill =
            await _billRepository.GetByIdAsync(
                request.Id,
                cancellationToken);


        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {request.Id} was not found.");


        var totalPaid =
            await _paymentRepository.GetTotalPaidAmountAsync(
                request.Id,
                cancellationToken);


        if (request.TotalAmount < totalPaid)
        {
            throw new ConflictException(
                $"Bill total amount cannot be less than the amount already paid. " +
                $"Amount paid: {totalPaid:0.00}.");
        }


        bill.Update(
            request.TotalAmount);


        bill.UpdatePaymentStatus(
            totalPaid);


        await _billRepository.SaveChangesAsync(
            cancellationToken);


        return true;
    }
}