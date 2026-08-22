using MediatR;

namespace MediCore.Application.Features.Billing.Commands.DeleteBillItem;

public record DeleteBillItemCommand(int Id) : IRequest<bool>;