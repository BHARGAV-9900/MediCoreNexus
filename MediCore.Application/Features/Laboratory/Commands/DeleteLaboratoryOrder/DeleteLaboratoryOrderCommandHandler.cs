using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryOrder;

public class DeleteLaboratoryOrderCommandHandler
    : IRequestHandler<DeleteLaboratoryOrderCommand, bool>
{
    private readonly ILaboratoryOrderRepository _repository;

    public DeleteLaboratoryOrderCommandHandler(
        ILaboratoryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteLaboratoryOrderCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryOrder = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryOrder is null)
            throw new NotFoundException(
                $"Laboratory order with Id {request.Id} was not found.");

        laboratoryOrder.Delete();

        await _repository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}