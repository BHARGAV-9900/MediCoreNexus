using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandler
    : IRequestHandler<CreateMedicineCommand, int>
{
    private readonly IMedicineRepository _medicineRepository;

    public CreateMedicineCommandHandler(
        IMedicineRepository medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    public async Task<int> Handle(
        CreateMedicineCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _medicineRepository.ExistsByNameAsync(
            request.Name.Trim(),
            cancellationToken);

        if (exists)
            throw new ConflictException(
                $"Medicine '{request.Name}' already exists.");

        var medicine = new Medicine(
            request.Name,
            request.Manufacturer,
            request.UnitPrice);

        await _medicineRepository.AddAsync(
            medicine,
            cancellationToken);

        await _medicineRepository.SaveChangesAsync(
            cancellationToken);

        return medicine.Id;
    }
}