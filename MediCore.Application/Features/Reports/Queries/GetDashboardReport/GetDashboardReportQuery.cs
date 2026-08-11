using MediatR;

namespace MediCore.Application.Features.Reports.Queries.GetDashboardReport;

public record GetDashboardReportQuery
    : IRequest<DashboardReportDto>;