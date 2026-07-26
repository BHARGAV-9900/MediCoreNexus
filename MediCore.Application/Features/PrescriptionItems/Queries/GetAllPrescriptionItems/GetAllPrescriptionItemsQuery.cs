using MediatR;

namespace MediCore.Application.Features.PrescriptionItems.Queries.GetAllPrescriptionItems;

public record GetAllPrescriptionItemsQuery
    : IRequest<IEnumerable<PrescriptionItemDto>>;