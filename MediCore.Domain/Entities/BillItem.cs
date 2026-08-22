using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class BillItem : BaseAuditableEntity
{
    private BillItem()
    {
    }

    public BillItem(
        int billId,
        string description,
        int quantity,
        decimal unitPrice)
    {
        SetBill(billId);
        SetDescription(description);
        SetQuantity(quantity);
        SetUnitPrice(unitPrice);
    }

    public int BillId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalAmount => Quantity * UnitPrice;

    public Bill? Bill { get; private set; }

    private void SetBill(int billId)
    {
        if (billId <= 0)
            throw new ArgumentException("Invalid bill.");

        BillId = billId;
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.");

        Description = description.Trim();
    }

    private void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Quantity = quantity;
    }

    private void SetUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero.");

        UnitPrice = unitPrice;
    }

    public void Update(
        string description,
        int quantity,
        decimal unitPrice)
    {
        SetDescription(description);
        SetQuantity(quantity);
        SetUnitPrice(unitPrice);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}