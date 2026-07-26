using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Commands.DeletePayment;

public class DeletePaymentCommandHandler
    : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;

    public DeletePaymentCommandHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<bool> Handle(
        DeletePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (payment is null)
            throw new NotFoundException(
                $"Payment with Id {request.Id} was not found.");

        payment.Delete();

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}