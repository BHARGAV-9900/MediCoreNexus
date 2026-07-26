using MediatR;

namespace MediCore.Application.Features.PrescriptionItems.Queries.GetPrescriptionItemById;

public record GetPrescriptionItemByIdQuery(int Id)
    : IRequest<PrescriptionItemDto>;