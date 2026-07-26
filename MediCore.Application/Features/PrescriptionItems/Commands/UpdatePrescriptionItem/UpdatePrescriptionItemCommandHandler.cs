using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.PrescriptionItems.Commands.UpdatePrescriptionItem;

public class UpdatePrescriptionItemCommandHandler
    : IRequestHandler<UpdatePrescriptionItemCommand, bool>
{
    private readonly IPrescriptionItemRepository _repository;

    public UpdatePrescriptionItemCommandHandler(
        IPrescriptionItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdatePrescriptionItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new NotFoundException(
                $"Prescription Item with Id {request.Id} was not found.");

        item.Update(
            request.Dosage,
            request.Frequency,
            request.DurationInDays,
            request.Quantity);

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}