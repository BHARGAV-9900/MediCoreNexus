using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Inventory : BaseAuditableEntity
{
    private Inventory()
    {
    }

    public Inventory(
        int medicineId,
        string batchNumber,
        int quantityInStock,
        int minimumStockLevel,
        DateTime expiryDate,
        string supplier,
        string storageLocation)
    {
        SetMedicine(medicineId);
        SetBatchNumber(batchNumber);
        SetQuantity(quantityInStock);
        SetMinimumStock(minimumStockLevel);

        ExpiryDate = expiryDate;
        Supplier = supplier;
        StorageLocation = storageLocation;

        IsActive = true;
    }

    public int MedicineId { get; private set; }

    public string BatchNumber { get; private set; } = string.Empty;

    public int QuantityInStock { get; private set; }

    public int MinimumStockLevel { get; private set; }

    public DateTime ExpiryDate { get; private set; }

    public string Supplier { get; private set; } = string.Empty;

    public string StorageLocation { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public Medicine? Medicine { get; private set; }

    public bool IsLowStock =>
        QuantityInStock <= MinimumStockLevel;

    public bool IsExpired =>
        ExpiryDate.Date < DateTime.UtcNow.Date;

    public void UpdateStock(int quantity)
    {
        QuantityInStock = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
        QuantityInStock += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity > QuantityInStock)
            throw new InvalidOperationException(
                "Insufficient stock.");

        QuantityInStock -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string batchNumber,
        int minimumStockLevel,
        DateTime expiryDate,
        string supplier,
        string storageLocation)
    {
        SetBatchNumber(batchNumber);
        SetMinimumStock(minimumStockLevel);

        ExpiryDate = expiryDate;
        Supplier = supplier;
        StorageLocation = storageLocation;

        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetMedicine(int medicineId)
    {
        if (medicineId <= 0)
            throw new ArgumentException("Invalid medicine.");

        MedicineId = medicineId;
    }

    private void SetBatchNumber(string batchNumber)
    {
        if (string.IsNullOrWhiteSpace(batchNumber))
            throw new ArgumentException("Batch number is required.");

        BatchNumber = batchNumber.Trim();
    }

    private void SetQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.");

        QuantityInStock = quantity;
    }

    private void SetMinimumStock(int minimumStock)
    {
        if (minimumStock < 0)
            throw new ArgumentException("Minimum stock cannot be negative.");

        MinimumStockLevel = minimumStock;
    }
}