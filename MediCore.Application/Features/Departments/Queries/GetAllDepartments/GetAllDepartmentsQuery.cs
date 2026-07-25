using MediatR;

namespace MediCore.Application.Features.Departments.Queries.GetAllDepartments;

public sealed record GetAllDepartmentsQuery()
    : IRequest<IEnumerable<DepartmentDto>>;