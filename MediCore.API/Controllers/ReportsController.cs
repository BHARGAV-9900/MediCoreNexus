using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Reports;
using MediCore.Application.Features.Reports.Queries.GetDashboardReport;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Policy = "ReportsManagement")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(
        CancellationToken cancellationToken)
    {
        var report =
            await _mediator.Send(
                new GetDashboardReportQuery(),
                cancellationToken);

        return Ok(
            ApiResponse<DashboardReportDto>
                .SuccessResponse(
                    report,
                    "Dashboard report retrieved successfully."));
    }
}