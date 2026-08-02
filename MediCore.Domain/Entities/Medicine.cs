using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Medicine : BaseAuditableEntity
{
    private Medicine()
    {
    }

    public Medicine(
        string name,
        string manufacturer,
        decimal unitPrice)
    {
        SetName(name);
        SetUnitPrice(unitPrice);

        Manufacturer = manufacturer;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public string Manufacturer { get; private set; } = string.Empty;

    public decimal UnitPrice { get; private set; }

    public bool IsActive { get; private set; }
    
    public ICollection<PrescriptionItem> PrescriptionItems { get; private set; }
        = new List<PrescriptionItem>();

    public ICollection<Inventory> Inventories { get; private set; }
    = new List<Inventory>();

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Medicine name is required.");

        Name = name.Trim();
    }
    private void SetUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero.");

        UnitPrice = unitPrice;
    }
    public void Update(
        string name,
        string manufacturer,
        decimal unitPrice)
    {
        SetName(name);
        SetUnitPrice(unitPrice);

        Manufacturer = manufacturer;

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
}