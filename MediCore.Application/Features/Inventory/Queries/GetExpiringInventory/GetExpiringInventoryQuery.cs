using MediatR;

namespace MediCore.Application.Features.Inventory.Queries.GetExpiringInventory;

public record GetExpiringInventoryQuery(
    int Days)
    : IRequest<IEnumerable<InventoryDto>>;