using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class User : BaseAuditableEntity
{
    private User()
    {
    }

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        int roleId)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);

        PasswordHash = passwordHash;
        RoleId = roleId;

        IsActive = true;
    }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public int RoleId { get; private set; }

    public Role? Role { get; private set; }

    public ICollection<RefreshToken> RefreshTokens { get; private set; }
        = new List<RefreshToken>();

    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        FirstName = firstName.Trim();
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        LastName = lastName.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Email = email.Trim().ToLowerInvariant();
    }

    public void Update(
        string firstName,
        string lastName,
        string email,
        int roleId)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);

        RoleId = roleId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}