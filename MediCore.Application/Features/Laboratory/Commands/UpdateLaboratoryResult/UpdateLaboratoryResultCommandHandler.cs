using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryResult;

public class UpdateLaboratoryResultCommandHandler
    : IRequestHandler<UpdateLaboratoryResultCommand, bool>
{
    private readonly ILaboratoryResultRepository _repository;

    public UpdateLaboratoryResultCommandHandler(
        ILaboratoryResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryResult = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryResult is null)
            throw new NotFoundException(
                $"Laboratory result with Id {request.Id} was not found.");

        laboratoryResult.Update(
            request.Result,
            request.Remarks);

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}