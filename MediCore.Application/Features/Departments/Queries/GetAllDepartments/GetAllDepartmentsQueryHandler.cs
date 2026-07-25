using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Departments.Queries.GetAllDepartments;

public class GetAllDepartmentsQueryHandler
    : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
{
    private readonly IDepartmentRepository _repository;

    public GetAllDepartmentsQueryHandler(
        IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DepartmentDto>> Handle(
    GetAllDepartmentsQuery request,
    CancellationToken cancellationToken)
    {
        var departments = await _repository.GetAllAsync(
            cancellationToken);

        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        });
    }
}