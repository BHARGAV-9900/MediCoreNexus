using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryTests;

public class GetAllLaboratoryTestsQueryHandler
    : IRequestHandler<GetAllLaboratoryTestsQuery, IEnumerable<LaboratoryTestDto>>
{
    private readonly ILaboratoryTestRepository _repository;

    public GetAllLaboratoryTestsQueryHandler(
        ILaboratoryTestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LaboratoryTestDto>> Handle(
        GetAllLaboratoryTestsQuery request,
        CancellationToken cancellationToken)
    {
        var tests = await _repository.GetAllAsync(cancellationToken);

        return tests.Select(t => new LaboratoryTestDto
        {
            Id = t.Id,
            PublicId = t.PublicId,
            Name = t.Name,
            Price = t.Price,
            Description = t.Description,
            IsActive = t.IsActive
        });
    }
}