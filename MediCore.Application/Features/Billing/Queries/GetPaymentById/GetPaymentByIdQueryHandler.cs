using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler
    : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdQueryHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentDto> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (payment is null)
            throw new NotFoundException(
                $"Payment with Id {request.Id} was not found.");

        return new PaymentDto
        {
            Id = payment.Id,
            PublicId = payment.PublicId,
            BillId = payment.BillId,
            BillPublicId = payment.Bill!.PublicId,
            Amount = payment.Amount,
            PaymentMethod = payment.PaymentMethod,
            PaidOn = payment.PaidOn
        };
    }
}