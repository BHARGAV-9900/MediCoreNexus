namespace MediCore.Application.Features.AuditLogs.DTOs;

public class AuditLogDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public int? UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? Role { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string? EntityPublicId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? IpAddress { get; set; }

    public string? RequestPath { get; set; }

    public string? RequestId { get; set; }

    public DateTime OccurredAtUtc { get; set; }
}
