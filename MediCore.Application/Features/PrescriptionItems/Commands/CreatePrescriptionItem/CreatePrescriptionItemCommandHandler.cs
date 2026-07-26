using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.PrescriptionItems.Commands.CreatePrescriptionItem;

public class CreatePrescriptionItemCommandHandler
    : IRequestHandler<CreatePrescriptionItemCommand, int>
{
    private readonly IPrescriptionItemRepository _prescriptionItemRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMedicineRepository _medicineRepository;

    public CreatePrescriptionItemCommandHandler(
        IPrescriptionItemRepository prescriptionItemRepository,
        IPrescriptionRepository prescriptionRepository,
        IMedicineRepository medicineRepository)
    {
        _prescriptionItemRepository = prescriptionItemRepository;
        _prescriptionRepository = prescriptionRepository;
        _medicineRepository = medicineRepository;
    }

    public async Task<int> Handle(
        CreatePrescriptionItemCommand request,
        CancellationToken cancellationToken)
    {
        // Ensure prescription exists
        var prescription = await _prescriptionRepository.GetByIdAsync(
            request.PrescriptionId,
            cancellationToken);

        if (prescription is null)
            throw new NotFoundException(
                $"Prescription with Id {request.PrescriptionId} was not found.");

        // Ensure medicine exists
        var medicine = await _medicineRepository.GetByIdAsync(
            request.MedicineId,
            cancellationToken);

        if (medicine is null)
            throw new NotFoundException(
                $"Medicine with Id {request.MedicineId} was not found.");

        // Prevent duplicate medicine in the same prescription
        var exists = await _prescriptionItemRepository.ExistsAsync(
            request.PrescriptionId,
            request.MedicineId,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                "This medicine has already been added to the prescription.");

        var prescriptionItem = new PrescriptionItem(
            request.PrescriptionId,
            request.MedicineId,
            request.Dosage,
            request.Frequency,
            request.DurationInDays,
            request.Quantity);

        await _prescriptionItemRepository.AddAsync(
            prescriptionItem,
            cancellationToken);

        await _prescriptionItemRepository.SaveChangesAsync(
            cancellationToken);

        return prescriptionItem.Id;
    }
}