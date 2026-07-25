using MediatR;
using MediCore.Application.Features.Departments.Queries.GetAllDepartments;

namespace MediCore.Application.Features.Departments.Queries.GetDepartmentById;

public sealed record GetDepartmentByIdQuery(int Id)
    : IRequest<DepartmentDto>;