using MediatR;

namespace MediCore.Application.Features.Inventory.Queries.GetAllInventory;

public record GetAllInventoryQuery()
    : IRequest<IEnumerable<InventoryDto>>;