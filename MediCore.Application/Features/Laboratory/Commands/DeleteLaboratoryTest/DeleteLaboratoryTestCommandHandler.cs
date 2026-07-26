using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryTest;

public class DeleteLaboratoryTestCommandHandler
    : IRequestHandler<DeleteLaboratoryTestCommand, bool>
{
    private readonly ILaboratoryTestRepository _repository;

    public DeleteLaboratoryTestCommandHandler(
        ILaboratoryTestRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteLaboratoryTestCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryTest = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryTest is null)
            throw new NotFoundException(
                $"Laboratory test with Id {request.Id} was not found.");

        laboratoryTest.Delete();

        await _repository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}