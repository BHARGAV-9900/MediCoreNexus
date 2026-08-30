using MediatR;
using MediCore.Application.Features.AuditLogs.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.AuditLogs.Queries.GetAuditLogs;

public class GetAuditLogsQueryHandler
    : IRequestHandler<GetAuditLogsQuery, AuditLogPageDto>
{
    private readonly IAuditLogRepository _auditLogRepository;

    public GetAuditLogsQueryHandler(IAuditLogRepository auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    public async Task<AuditLogPageDto> Handle(
        GetAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(1, request.PageNumber);
        var pageSize = Math.Clamp(request.PageSize, 10, 100);

        var (items, totalCount) =
            await _auditLogRepository.GetPagedAsync(
                pageNumber,
                pageSize,
                request.EntityName,
                request.Action,
                request.UserId,
                request.FromUtc,
                request.ToUtc,
                cancellationToken);

        return new AuditLogPageDto
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items.Select(item => new AuditLogDto
            {
                Id = item.Id,
                PublicId = item.PublicId,
                UserId = item.UserId,
                UserEmail = item.UserEmail,
                Role = item.Role,
                Action = item.Action,
                EntityName = item.EntityName,
                EntityPublicId = item.EntityPublicId,
                OldValues = item.OldValues,
                NewValues = item.NewValues,
                IpAddress = item.IpAddress,
                RequestPath = item.RequestPath,
                RequestId = item.RequestId,
                OccurredAtUtc = item.OccurredAtUtc
            }).ToList()
        };
    }
}
