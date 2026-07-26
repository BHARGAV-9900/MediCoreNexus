using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommandHandler
    : IRequestHandler<UpdatePrescriptionCommand, bool>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public UpdatePrescriptionCommandHandler(
        IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<bool> Handle(
        UpdatePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prescription is null)
            throw new NotFoundException(
                $"Prescription with Id {request.Id} was not found.");

        prescription.Update(
            request.Instructions,
            request.Notes);

        await _prescriptionRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}