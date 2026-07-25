namespace MediCore.Domain.Entities;

using MediCore.Domain.Common;

public class Department : BaseAuditableEntity
{
    // Required by Entity Framework Core
    private Department()
    {
    }
    
    public Department(string name, string? description = null)
    {
        SetName(name);
        Description = description;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<Doctor> Doctors { get; private set; } = new List<Doctor>();
    public void Rename(string name)
    {
        SetName(name);
    }
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
    public void UpdateDescription(string? description)
    {
        Description = description;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name cannot be empty.");

        if (name.Length > 100)
            throw new ArgumentException("Department name cannot exceed 100 characters.");

        Name = name.Trim();
    }
}