using MediCore.Application.Features.Dashboard.Queries.GetDashboard;

namespace MediCore.Application.Interfaces.Repositories;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardAsync(
        CancellationToken cancellationToken = default);
}