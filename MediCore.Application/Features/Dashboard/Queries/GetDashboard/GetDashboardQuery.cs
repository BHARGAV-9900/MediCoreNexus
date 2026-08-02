using MediatR;

namespace MediCore.Application.Features.Dashboard.Queries.GetDashboard;

public record GetDashboardQuery()
    : IRequest<DashboardDto>;