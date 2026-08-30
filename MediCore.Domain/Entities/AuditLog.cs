using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class AuditLog : BaseEntity
{
    private AuditLog()
    {
    }

    public AuditLog(
        int? userId,
        string? userEmail,
        string? role,
        string action,
        string entityName,
        string? entityPublicId,
        string? oldValues,
        string? newValues,
        string? ipAddress,
        string? requestPath,
        string? requestId,
        DateTime occurredAtUtc)
    {
        UserId = userId;
        UserEmail = userEmail;
        Role = role;
        Action = action;
        EntityName = entityName;
        EntityPublicId = entityPublicId;
        OldValues = oldValues;
        NewValues = newValues;
        IpAddress = ipAddress;
        RequestPath = requestPath;
        RequestId = requestId;
        OccurredAtUtc = occurredAtUtc;
    }

    public int? UserId { get; private set; }

    public string? UserEmail { get; private set; }

    public string? Role { get; private set; }

    public string Action { get; private set; } = string.Empty;

    public string EntityName { get; private set; } = string.Empty;

    public string? EntityPublicId { get; private set; }

    public string? OldValues { get; private set; }

    public string? NewValues { get; private set; }

    public string? IpAddress { get; private set; }

    public string? RequestPath { get; private set; }

    public string? RequestId { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }
}
