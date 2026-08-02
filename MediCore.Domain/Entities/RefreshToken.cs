using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class RefreshToken : BaseAuditableEntity
{
    private RefreshToken()
    {
    }

    public RefreshToken(
        int userId,
        string token,
        DateTime expiresOn)
    {
        UserId = userId;
        Token = token;
        ExpiresOn = expiresOn;
    }

    public int UserId { get; private set; }

    public string Token { get; private set; } = string.Empty;

    public DateTime ExpiresOn { get; private set; }

    public bool IsRevoked { get; private set; }

    public User User { get; private set; } = null!;

    public void Revoke()
    {
        IsRevoked = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}