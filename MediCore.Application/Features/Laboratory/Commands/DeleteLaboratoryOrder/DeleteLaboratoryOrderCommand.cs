using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryOrder;

public record DeleteLaboratoryOrderCommand(int Id) : IRequest<bool>;