using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class LaboratoryTest : BaseAuditableEntity
{
    private LaboratoryTest()
    {
    }

    public LaboratoryTest(
        string name,
        decimal price,
        string? description = null)
    {
        SetName(name);
        SetPrice(price);
        SetDescription(description);

        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<LaboratoryOrder> LaboratoryOrders { get; private set; }
        = new List<LaboratoryOrder>();

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Test name is required.");

        Name = name.Trim();
    }
    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        Price = price;
    }
    private void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
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
    public void Update(
        string name,
        decimal price,
        string? description)
    {
        SetName(name);
        SetPrice(price);
        SetDescription(description);

        UpdatedAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}