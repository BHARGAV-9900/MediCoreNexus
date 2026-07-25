using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandler
    : IRequestHandler<UpdateMedicineCommand, bool>
{
    private readonly IMedicineRepository _medicineRepository;

    public UpdateMedicineCommandHandler(
        IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public async Task<bool> Handle(
        UpdateMedicineCommand request,
        CancellationToken cancellationToken)
    {
        var medicine = await _medicineRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (medicine is null)
            throw new ArgumentException(
                $"Medicine with Id {request.Id} was not found.");

        medicine.Update(
            request.Name,
            request.Manufacturer,
            request.UnitPrice);

        await _medicineRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}