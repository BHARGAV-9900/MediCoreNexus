using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Dashboard.Queries.GetDashboard;

public class GetDashboardQueryHandler
    : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IDashboardRepository _dashboardRepository;

    public GetDashboardQueryHandler(
        IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardDto> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken)
    {
        return await _dashboardRepository.GetDashboardAsync(
            cancellationToken);
    }
}