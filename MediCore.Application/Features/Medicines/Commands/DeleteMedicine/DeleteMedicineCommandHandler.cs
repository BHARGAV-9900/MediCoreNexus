using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler
    : IRequestHandler<DeleteMedicineCommand, bool>
{
    private readonly IMedicineRepository _medicineRepository;

    public DeleteMedicineCommandHandler(
        IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public async Task<bool> Handle(
        DeleteMedicineCommand request,
        CancellationToken cancellationToken)
    {
        var medicine = await _medicineRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (medicine is null)
            throw new ArgumentException(
                $"Medicine with Id {request.Id} was not found.");

        medicine.Delete();

        await _medicineRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}