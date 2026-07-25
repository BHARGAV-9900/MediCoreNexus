using MediatR;

namespace MediCore.Application.Features.Departments.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(
    string Name,
    string? Description
) : IRequest<int>;