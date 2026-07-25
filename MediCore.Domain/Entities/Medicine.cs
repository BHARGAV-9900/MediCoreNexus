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

        Manufacturer = manufacturer;
        UnitPrice = unitPrice;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public string Manufacturer { get; private set; } = string.Empty;

    public decimal UnitPrice { get; private set; }

    public bool IsActive { get; private set; }
    
    public ICollection<PrescriptionItem> PrescriptionItems { get; private set; }
        = new List<PrescriptionItem>();
   

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Medicine name is required.");

        Name = name.Trim();
    }
}