using MediatR;
using MediCore.Application.Features.Reports;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Reports.Queries.GetDashboardReport;

public class GetDashboardReportQueryHandler
    : IRequestHandler<
        GetDashboardReportQuery,
        DashboardReportDto>
{
    private readonly IReportRepository _reportRepository;

    public GetDashboardReportQueryHandler(
        IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<DashboardReportDto> Handle(
        GetDashboardReportQuery request,
        CancellationToken cancellationToken)
    {
        return await _reportRepository
            .GetDashboardReportAsync(
                cancellationToken);
    }
}