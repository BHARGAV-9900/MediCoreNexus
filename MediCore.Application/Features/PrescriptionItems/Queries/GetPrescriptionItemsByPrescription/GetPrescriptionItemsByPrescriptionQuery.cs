using MediatR;
using MediCore.Application.Features.PrescriptionItems.Queries;

namespace MediCore.Application.Features.PrescriptionItems.Queries.GetPrescriptionItemsByPrescription;

public record GetPrescriptionItemsByPrescriptionQuery(int PrescriptionId)
    : IRequest<IEnumerable<PrescriptionItemDto>>;