using MediatR;

namespace MediCore.Application.Features.Departments.Commands.DeleteDepartment;

public sealed record DeleteDepartmentCommand(int Id)
    : IRequest;