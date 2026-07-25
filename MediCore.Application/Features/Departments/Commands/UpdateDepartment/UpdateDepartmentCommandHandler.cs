using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler
    : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _repository;

    public UpdateDepartmentCommandHandler(
        IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
    UpdateDepartmentCommand request,
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

        var duplicateDepartment = (await _repository.GetAllAsync(
            cancellationToken))
            .FirstOrDefault(d =>
                d.Name.ToLower() == request.Name.ToLower()
                && d.Id != request.Id);

        if (duplicateDepartment is not null)
        {
            throw new ConflictException(
                "Department already exists.");
        }

        department.Update(request.Name, request.Description);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }
}