using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;

public class GetPrescriptionByIdQueryHandler
    : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetPrescriptionByIdQueryHandler(
        IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<PrescriptionDto> Handle(
        GetPrescriptionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prescription is null)
            throw new NotFoundException(
                $"Prescription with Id {request.Id} was not found.");

        return new PrescriptionDto
        {
            Id = prescription.Id,
            PublicId = prescription.PublicId,
            AppointmentId = prescription.AppointmentId,
            AppointmentPublicId = prescription.Appointment!.PublicId,
            Instructions = prescription.Instructions,
            Notes = prescription.Notes
        };
    }
}