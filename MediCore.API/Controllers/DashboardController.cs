using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Dashboard.Queries.GetDashboard;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var dashboard = await _mediator.Send(
            new GetDashboardQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<DashboardDto>.SuccessResponse(
                dashboard,
                "Dashboard data retrieved successfully."));
    }
}