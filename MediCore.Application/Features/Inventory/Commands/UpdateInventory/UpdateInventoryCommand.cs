using MediatR;

namespace MediCore.Application.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommand : IRequest<bool>
{
    public int Id { get; set; }

    public string BatchNumber { get; set; } = string.Empty;

    public int MinimumStockLevel { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Supplier { get; set; } = string.Empty;

    public string StorageLocation { get; set; } = string.Empty;
}