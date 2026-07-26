using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Billing.Commands.CreatePayment;

public class CreatePaymentCommandHandler
    : IRequestHandler<CreatePaymentCommand, int>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBillRepository _billRepository;

    public CreatePaymentCommandHandler(
        IPaymentRepository paymentRepository,
        IBillRepository billRepository)
    {
        _paymentRepository = paymentRepository;
        _billRepository = billRepository;
    }

    public async Task<int> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(
            request.BillId,
            cancellationToken);

        if (bill is null)
            throw new NotFoundException(
                $"Bill with Id {request.BillId} was not found.");

        if (bill.IsPaid)
            throw new ConflictException(
                "This bill has already been paid.");

        var totalPaid =
            await _paymentRepository.GetTotalPaidAmountAsync(
                request.BillId,
                cancellationToken);

        var remainingBalance =
            bill.TotalAmount - totalPaid;

        if (request.Amount > remainingBalance)
            throw new ConflictException(
                "Payment amount exceeds the remaining balance.");

        var payment = new Payment(
            request.BillId,
            request.Amount,
            request.PaymentMethod);

        await _paymentRepository.AddAsync(
            payment,
            cancellationToken);

        if (request.Amount == remainingBalance)
        {
            bill.MarkAsPaid();
        }

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);

        return payment.Id;
    }
}