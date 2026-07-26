using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryResults;

public record GetAllLaboratoryResultsQuery
    : IRequest<IEnumerable<LaboratoryResultDto>>;