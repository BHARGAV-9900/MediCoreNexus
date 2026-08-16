using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.UpdatePayment;

public class UpdatePaymentCommandHandler
    : IRequestHandler<UpdatePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    private readonly IBillRepository _billRepository;


    public UpdatePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        IBillRepository billRepository)
    {
        _paymentRepository = paymentRepository;

        _billRepository = billRepository;
    }


    public async Task<bool> Handle(
        UpdatePaymentCommand request,
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


        var totalPaid =
            await _paymentRepository.GetTotalPaidAmountAsync(
                payment.BillId,
                cancellationToken);


        // Remove the current payment from the calculation
        var totalPaidWithoutCurrentPayment =
            totalPaid - payment.Amount;


        var newTotalPaid =
            totalPaidWithoutCurrentPayment +
            request.Amount;


        if (newTotalPaid > bill.TotalAmount)
        {
            throw new ConflictException(
                $"Payment amount exceeds the remaining balance. " +
                $"Maximum allowed amount: " +
                $"{bill.TotalAmount - totalPaidWithoutCurrentPayment:0.00}.");
        }


        payment.Update(
            request.Amount,
            request.PaymentMethod);


        bill.UpdatePaymentStatus(
            newTotalPaid);


        await _paymentRepository.SaveChangesAsync(
            cancellationToken);


        return true;
    }
}