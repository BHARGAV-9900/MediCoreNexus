using MediCore.Application.Features.Reports;

namespace MediCore.Application.Interfaces.Repositories;

public interface IReportRepository
{
    Task<DashboardReportDto> GetDashboardReportAsync(
        CancellationToken cancellationToken);
}