using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Billing.Queries.GetPaymentsByBill;

public class GetPaymentsByBillQueryHandler
    : IRequestHandler<GetPaymentsByBillQuery, IEnumerable<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentsByBillQueryHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IEnumerable<PaymentDto>> Handle(
        GetPaymentsByBillQuery request,
        CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetByBillIdAsync(
            request.BillId,
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