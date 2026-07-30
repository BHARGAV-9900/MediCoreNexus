using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Role : BaseAuditableEntity
{
    private Role()
    {
    }

    public Role(string name)
    {
        SetName(name);
    }

    public string Name { get; private set; } = string.Empty;

    public ICollection<User> Users { get; private set; }
        = new List<User>();

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name is required.");

        Name = name.Trim();
    }

    public void Update(string name)
    {
        SetName(name);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}