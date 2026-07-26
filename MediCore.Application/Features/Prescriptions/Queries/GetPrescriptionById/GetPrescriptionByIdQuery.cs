using MediatR;

namespace MediCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;

public record GetPrescriptionByIdQuery(int Id)
    : IRequest<PrescriptionDto>;