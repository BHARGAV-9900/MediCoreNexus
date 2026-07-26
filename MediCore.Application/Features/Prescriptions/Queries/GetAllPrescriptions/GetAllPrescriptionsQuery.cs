using MediatR;

namespace MediCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;

public record GetAllPrescriptionsQuery
    : IRequest<IEnumerable<PrescriptionDto>>;