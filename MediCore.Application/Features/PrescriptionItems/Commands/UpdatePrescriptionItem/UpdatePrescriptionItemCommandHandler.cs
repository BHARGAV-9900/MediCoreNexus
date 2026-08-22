using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.PrescriptionItems.Commands.UpdatePrescriptionItem;

public class UpdatePrescriptionItemCommandHandler
    : IRequestHandler<UpdatePrescriptionItemCommand, bool>
{
    private readonly IPrescriptionItemRepository _prescriptionItemRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMedicineRepository _medicineRepository;

    public UpdatePrescriptionItemCommandHandler(
        IPrescriptionItemRepository prescriptionItemRepository,
        IPrescriptionRepository prescriptionRepository,
        IMedicineRepository medicineRepository)
    {
        _prescriptionItemRepository = prescriptionItemRepository;
        _prescriptionRepository = prescriptionRepository;
        _medicineRepository = medicineRepository;
    }

    public async Task<bool> Handle(
        UpdatePrescriptionItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _prescriptionItemRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new NotFoundException(
                $"Prescription Item with Id {request.Id} was not found.");

        var prescription = await _prescriptionRepository.GetByIdAsync(
            request.PrescriptionId,
            cancellationToken);

        if (prescription is null)
            throw new NotFoundException(
                $"Prescription with Id {request.PrescriptionId} was not found.");

        var medicine = await _medicineRepository.GetByIdAsync(
            request.MedicineId,
            cancellationToken);

        if (medicine is null)
            throw new NotFoundException(
                $"Medicine with Id {request.MedicineId} was not found.");

        var exists = await _prescriptionItemRepository.ExistsAsync(
            request.PrescriptionId,
            request.MedicineId,
            cancellationToken,
            request.Id);

        if (exists)
            throw new ConflictException(
                "This medicine has already been added to the prescription.");

        item.Update(
            request.PrescriptionId,
            request.MedicineId,
            request.Dosage,
            request.Frequency,
            request.DurationInDays,
            request.Quantity);

        await _prescriptionItemRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}