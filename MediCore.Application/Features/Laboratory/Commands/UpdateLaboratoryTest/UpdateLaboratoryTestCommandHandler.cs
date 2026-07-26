using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryTest;

public class UpdateLaboratoryTestCommandHandler
    : IRequestHandler<UpdateLaboratoryTestCommand, bool>
{
    private readonly ILaboratoryTestRepository _repository;

    public UpdateLaboratoryTestCommandHandler(
        ILaboratoryTestRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateLaboratoryTestCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryTest = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryTest is null)
            throw new NotFoundException(
                $"Laboratory test with Id {request.Id} was not found.");

        laboratoryTest.Update(
            request.Name,
            request.Price,
            request.Description);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}