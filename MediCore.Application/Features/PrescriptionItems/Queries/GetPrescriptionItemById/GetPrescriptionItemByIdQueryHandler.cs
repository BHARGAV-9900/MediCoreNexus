using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.PrescriptionItems.Queries.GetPrescriptionItemById;

public class GetPrescriptionItemByIdQueryHandler
    : IRequestHandler<GetPrescriptionItemByIdQuery, PrescriptionItemDto>
{
    private readonly IPrescriptionItemRepository _repository;

    public GetPrescriptionItemByIdQueryHandler(
        IPrescriptionItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<PrescriptionItemDto> Handle(
        GetPrescriptionItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new NotFoundException(
                $"Prescription Item with Id {request.Id} was not found.");

        return new PrescriptionItemDto
        {
            Id = item.Id,
            PublicId = item.PublicId,
            PrescriptionId = item.PrescriptionId,
            PrescriptionPublicId = item.Prescription!.PublicId,
            MedicineId = item.MedicineId,
            MedicinePublicId = item.Medicine!.PublicId,
            MedicineName = item.Medicine.Name,
            Dosage = item.Dosage,
            Frequency = item.Frequency,
            DurationInDays = item.DurationInDays,
            Quantity = item.Quantity
        };
    }
}