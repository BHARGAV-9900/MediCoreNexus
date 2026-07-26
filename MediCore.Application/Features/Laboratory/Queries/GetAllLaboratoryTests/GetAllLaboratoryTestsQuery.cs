using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryTests;

public record GetAllLaboratoryTestsQuery
    : IRequest<IEnumerable<LaboratoryTestDto>>;