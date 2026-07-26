using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.PrescriptionItems.Commands.DeletePrescriptionItem;

public class DeletePrescriptionItemCommandHandler
    : IRequestHandler<DeletePrescriptionItemCommand, bool>
{
    private readonly IPrescriptionItemRepository _repository;

    public DeletePrescriptionItemCommandHandler(
        IPrescriptionItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeletePrescriptionItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new NotFoundException(
                $"Prescription Item with Id {request.Id} was not found.");

        item.Delete();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}