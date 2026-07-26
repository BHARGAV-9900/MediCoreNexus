using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryResult;

public record DeleteLaboratoryResultCommand(int Id)
    : IRequest<bool>;