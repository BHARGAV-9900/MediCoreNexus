using MediatR;

namespace MediCore.Application.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommand : IRequest<int>
{
    public int MedicineId { get; set; }

    public int QuantityInStock { get; set; }

    public int MinimumStockLevel { get; set; }

    public string BatchNumber { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public string Supplier { get; set; } = string.Empty;

    public string StorageLocation { get; set; } = string.Empty;
}