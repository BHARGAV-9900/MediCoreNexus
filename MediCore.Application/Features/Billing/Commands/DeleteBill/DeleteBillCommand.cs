using MediatR;

namespace MediCore.Application.Features.Billing.Commands.DeleteBill;

public record DeleteBillCommand(int Id) : IRequest<bool>;