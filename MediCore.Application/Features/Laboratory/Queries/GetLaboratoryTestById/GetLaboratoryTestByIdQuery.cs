using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryTestById;

public record GetLaboratoryTestByIdQuery(int Id)
    : IRequest<LaboratoryTestDto>;