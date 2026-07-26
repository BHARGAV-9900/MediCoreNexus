using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryHandler
    : IRequestHandler<GetMedicineByIdQuery, MedicineDto>
{
    private readonly IMedicineRepository _medicineRepository;

    public GetMedicineByIdQueryHandler(
        IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public async Task<MedicineDto> Handle(
        GetMedicineByIdQuery request,
        CancellationToken cancellationToken)
    {
        var medicine = await _medicineRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (medicine is null)
            throw new NotFoundException(
                $"Medicine with Id {request.Id} was not found.");

        return new MedicineDto
        {
            Id = medicine.Id,
            PublicId = medicine.PublicId,
            Name = medicine.Name,
            Manufacturer = medicine.Manufacturer,
            UnitPrice = medicine.UnitPrice,
            IsActive = medicine.IsActive
        };
    }
}