using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;

public class GetAllPrescriptionsQueryHandler
    : IRequestHandler<GetAllPrescriptionsQuery, IEnumerable<PrescriptionDto>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetAllPrescriptionsQueryHandler(
        IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<IEnumerable<PrescriptionDto>> Handle(
        GetAllPrescriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository.GetAllAsync(
            cancellationToken);

        return prescriptions.Select(p => new PrescriptionDto
        {
            Id = p.Id,
            PublicId = p.PublicId,
            AppointmentId = p.AppointmentId,
            AppointmentPublicId = p.Appointment!.PublicId,
            Instructions = p.Instructions,
            Notes = p.Notes
        });
    }
}