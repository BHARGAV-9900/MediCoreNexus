using MediatR;

namespace MediCore.Application.Features.Inventory.Queries.GetLowStockInventory;

public record GetLowStockInventoryQuery
    : IRequest<IEnumerable<InventoryDto>>;