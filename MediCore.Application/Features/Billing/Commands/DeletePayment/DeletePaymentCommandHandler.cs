using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.DeletePayment;

public class DeletePaymentCommandHandler
    : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    private readonly IBillRepository _billRepository;


    public DeletePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        IBillRepository billRepository)
    {
        _paymentRepository = paymentRepository;

        _billRepository = billRepository;
    }


    public async Task<bool> Handle(
        DeletePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(
                request.Id,
                cancellationToken);


        if (payment is null)
            throw new NotFoundException(
                $"Payment with Id {request.Id} was not found.");


        var bill =
            await _billRepository.GetByIdAsync(
                payment.BillId,
                cancellationToken);


        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {payment.BillId} was not found.");


        payment.Delete();


        // Get total after the payment has been soft deleted.
        // PaymentRepository ignores IsDeleted payments.
        var remainingTotalPaid =
            await _paymentRepository.GetTotalPaidAmountAsync(
                payment.BillId,
                cancellationToken);


        bill.UpdatePaymentStatus(
            remainingTotalPaid);


        await _paymentRepository.SaveChangesAsync(
            cancellationToken);


        return true;
    }
}