using MediatR;

namespace MediCore.Application.Features.Inventory.Queries.GetInventoryById;

public record GetInventoryByIdQuery(
    int Id)
    : IRequest<InventoryDto>;