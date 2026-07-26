using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.UpdatePayment;

public class UpdatePaymentCommandHandler
    : IRequestHandler<UpdatePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    public UpdatePaymentCommandHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<bool> Handle(
        UpdatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (payment is null)
            throw new NotFoundException(
                $"Payment with Id {request.Id} was not found.");

        payment.Update(
            request.Amount,
            request.PaymentMethod);

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}