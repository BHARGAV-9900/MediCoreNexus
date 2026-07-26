using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryResult;

public class DeleteLaboratoryResultCommandHandler
    : IRequestHandler<DeleteLaboratoryResultCommand, bool>
{
    private readonly ILaboratoryResultRepository _repository;

    public DeleteLaboratoryResultCommandHandler(
        ILaboratoryResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryResult = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryResult is null)
            throw new NotFoundException(
                $"Laboratory result with Id {request.Id} was not found.");

        laboratoryResult.Delete();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}