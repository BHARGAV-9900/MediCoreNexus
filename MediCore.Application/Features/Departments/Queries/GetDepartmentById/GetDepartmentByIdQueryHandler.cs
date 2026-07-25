using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Departments.Queries.GetAllDepartments;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQueryHandler
    : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
{
    private readonly IDepartmentRepository _repository;

    public GetDepartmentByIdQueryHandler(
        IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<DepartmentDto> Handle(
    GetDepartmentByIdQuery request,
    CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (department is null)
        {
            throw new NotFoundException(
                $"Department with Id {request.Id} was not found.");
        }

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }
}