using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetAllPayments;

public class GetAllPaymentsQueryHandler
    : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetAllPaymentsQueryHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IEnumerable<PaymentDto>> Handle(
        GetAllPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetAllAsync(
            cancellationToken);

        return payments.Select(p => new PaymentDto
        {
            Id = p.Id,
            PublicId = p.PublicId,
            BillId = p.BillId,
            BillPublicId = p.Bill!.PublicId,
            Amount = p.Amount,
            PaymentMethod = p.PaymentMethod,
            PaidOn = p.PaidOn
        });
    }
}