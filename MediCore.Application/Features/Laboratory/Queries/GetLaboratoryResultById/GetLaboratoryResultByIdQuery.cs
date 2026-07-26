using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryResultById;

public record GetLaboratoryResultByIdQuery(int Id)
    : IRequest<LaboratoryResultDto>;