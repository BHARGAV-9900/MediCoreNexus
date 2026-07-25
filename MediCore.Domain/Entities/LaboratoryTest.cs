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

        Price = price;
        Description = description;
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
}