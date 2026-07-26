using MediatR;
using MediCore.Application.Features.Billing.Queries;

namespace MediCore.Application.Features.Billing.Queries.GetPaymentsByBill;

public record GetPaymentsByBillQuery(int BillId)
    : IRequest<IEnumerable<PaymentDto>>;