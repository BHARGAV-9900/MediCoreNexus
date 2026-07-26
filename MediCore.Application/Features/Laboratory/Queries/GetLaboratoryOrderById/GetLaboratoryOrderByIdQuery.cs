using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryOrderById;

public record GetLaboratoryOrderByIdQuery(int Id)
    : IRequest<LaboratoryOrderDto>;