using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryTestById;

public class GetLaboratoryTestByIdQueryHandler
    : IRequestHandler<GetLaboratoryTestByIdQuery, LaboratoryTestDto>
{
    private readonly ILaboratoryTestRepository _repository;

    public GetLaboratoryTestByIdQueryHandler(
        ILaboratoryTestRepository repository)
    {
        _repository = repository;
    }

    public async Task<LaboratoryTestDto> Handle(
        GetLaboratoryTestByIdQuery request,
        CancellationToken cancellationToken)
    {
        var test = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (test is null)
            throw new NotFoundException(
                $"Laboratory test with Id {request.Id} was not found.");

        return new LaboratoryTestDto
        {
            Id = test.Id,
            PublicId = test.PublicId,
            Name = test.Name,
            Price = test.Price,
            Description = test.Description,
            IsActive = test.IsActive
        };
    }
}