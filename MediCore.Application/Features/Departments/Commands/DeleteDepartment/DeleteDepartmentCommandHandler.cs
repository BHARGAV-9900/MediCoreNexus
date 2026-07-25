using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler
    : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _repository;

    public async Task Handle(
    DeleteDepartmentCommand request,
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

        department.Delete();

        await _repository.SaveChangesAsync(
            cancellationToken);
    }
}