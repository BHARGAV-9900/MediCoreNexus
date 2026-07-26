using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryTest;

public record DeleteLaboratoryTestCommand(int Id) : IRequest<bool>;