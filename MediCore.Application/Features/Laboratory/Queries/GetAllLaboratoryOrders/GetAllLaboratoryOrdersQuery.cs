using MediatR;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryOrders;

public record GetAllLaboratoryOrdersQuery
    : IRequest<IEnumerable<LaboratoryOrderDto>>;