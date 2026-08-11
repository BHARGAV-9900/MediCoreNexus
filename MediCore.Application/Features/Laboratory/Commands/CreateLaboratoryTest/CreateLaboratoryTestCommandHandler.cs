using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryTest;

public class CreateLaboratoryTestCommandHandler
    : IRequestHandler<CreateLaboratoryTestCommand, int>
{
    private readonly ILaboratoryTestRepository _repository;

    public CreateLaboratoryTestCommandHandler(
        ILaboratoryTestRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
    CreateLaboratoryTestCommand request,
    CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();

        var exists = await _repository.ExistsByNameAsync(
            name,
            0,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                $"Laboratory test '{name}' already exists.");

        var laboratoryTest = new LaboratoryTest(
            name,
            request.Price,
            request.Description);

        await _repository.AddAsync(
            laboratoryTest,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return laboratoryTest.Id;
    }
}