using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<int> Handle(
    CreateDepartmentCommand request,
    CancellationToken cancellationToken)
    {
        var exists = await _departmentRepository.ExistsAsync(
            request.Name,
            cancellationToken);

        if (exists)
            throw new ConflictException("Department already exists.");

        var department = new Department(
            request.Name,
            request.Description);

        await _departmentRepository.AddAsync(
            department,
            cancellationToken);

        await _departmentRepository.SaveChangesAsync(
            cancellationToken);

        return department.Id;
    }
}