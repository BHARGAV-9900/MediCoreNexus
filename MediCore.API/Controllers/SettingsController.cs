using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Settings.Commands.UpdateSettings;
using MediCore.Application.Features.Settings.Queries.GetSettings;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SettingsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var settings =
            await _mediator.Send(
                new GetSettingsQuery(),
                cancellationToken);

        if (settings is null)
        {
            return NotFound(
                "Settings have not been configured yet.");
        }

        return Ok(
            ApiResponse<SettingsDto>.SuccessResponse(
                settings,
                "Settings retrieved successfully."));
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateSettingsCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Settings updated successfully."));
    }
}