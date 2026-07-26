using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Prescriptions.Commands.DeletePrescription;

public class DeletePrescriptionCommandHandler
    : IRequestHandler<DeletePrescriptionCommand, bool>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public DeletePrescriptionCommandHandler(
        IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<bool> Handle(
        DeletePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prescription is null)
            throw new NotFoundException(
                $"Prescription with Id {request.Id} was not found.");

        prescription.Delete();

        await _prescriptionRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}