using MediatR;
using MediCore.Application.Features.AuditLogs.DTOs;

namespace MediCore.Application.Features.AuditLogs.Queries.GetAuditLogs;

public class GetAuditLogsQuery : IRequest<AuditLogPageDto>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public string? EntityName { get; set; }

    public string? Action { get; set; }

    public int? UserId { get; set; }

    public DateTime? FromUtc { get; set; }

    public DateTime? ToUtc { get; set; }
}

public class AuditLogPageDto
{
    public IReadOnlyList<AuditLogDto> Items { get; set; }
        = Array.Empty<AuditLogDto>();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages =>
        PageSize <= 0
            ? 0
            : (int)Math.Ceiling((double)TotalCount / PageSize);
}
