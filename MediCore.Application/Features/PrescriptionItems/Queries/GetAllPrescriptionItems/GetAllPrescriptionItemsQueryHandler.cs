using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.PrescriptionItems.Queries.GetAllPrescriptionItems;

public class GetAllPrescriptionItemsQueryHandler
    : IRequestHandler<GetAllPrescriptionItemsQuery, IEnumerable<PrescriptionItemDto>>
{
    private readonly IPrescriptionItemRepository _repository;

    public GetAllPrescriptionItemsQueryHandler(
        IPrescriptionItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PrescriptionItemDto>> Handle(
        GetAllPrescriptionItemsQuery request,
        CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

        return items.Select(pi => new PrescriptionItemDto
        {
            Id = pi.Id,
            PublicId = pi.PublicId,
            PrescriptionId = pi.PrescriptionId,
            PrescriptionPublicId = pi.Prescription!.PublicId,
            MedicineId = pi.MedicineId,
            MedicinePublicId = pi.Medicine!.PublicId,
            MedicineName = pi.Medicine.Name,
            Dosage = pi.Dosage,
            Frequency = pi.Frequency,
            DurationInDays = pi.DurationInDays,
            Quantity = pi.Quantity
        });
    }
}