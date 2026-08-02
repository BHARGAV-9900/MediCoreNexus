namespace MediCore.Application.Features.Inventory;

public class InventoryDto
{
    public int Id { get; set; }

    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = string.Empty;

    public int QuantityInStock { get; set; }

    public int MinimumStockLevel { get; set; }

    public string BatchNumber { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }

    public string Supplier { get; set; } = string.Empty;

    public string StorageLocation { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}