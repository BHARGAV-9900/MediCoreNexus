using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Medicines.Queries.GetAllMedicines;

public class GetAllMedicinesQueryHandler
    : IRequestHandler<GetAllMedicinesQuery, IEnumerable<MedicineDto>>
{
    private readonly IMedicineRepository _medicineRepository;

    public GetAllMedicinesQueryHandler(
        IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public async Task<IEnumerable<MedicineDto>> Handle(
        GetAllMedicinesQuery request,
        CancellationToken cancellationToken)
    {
        var medicines = await _medicineRepository.GetAllAsync(
            cancellationToken);

        return medicines.Select(m => new MedicineDto
        {
            Id = m.Id,
            PublicId = m.PublicId,
            Name = m.Name,
            Manufacturer = m.Manufacturer,
            UnitPrice = m.UnitPrice,
            IsActive = m.IsActive
        });
    }
}