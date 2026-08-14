using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Notification;
using MediCore.Application.Features.Notification.Commands.CreateNotification;
using MediCore.Application.Features.Notification.Commands.DeleteNotification;
using MediCore.Application.Features.Notification.Commands.MarkAsRead;
using MediCore.Application.Features.Notification.Queries.GetAllNotifications;
using MediCore.Application.Features.Notification.Queries.GetNotificationById;
using MediCore.Application.Features.Notification.Queries.GetUnreadNotifications;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "NotificationManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateNotificationCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Notification created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var notifications = await _mediator.Send(
            new GetAllNotificationsQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<NotificationDto>>
                .SuccessResponse(
                    notifications,
                    "Notifications retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var notification = await _mediator.Send(
            new GetNotificationByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<NotificationDto>.SuccessResponse(
                notification,
                "Notification retrieved successfully."));
    }

    [HttpGet("unread")]
    public async Task<IActionResult> GetUnread(
        CancellationToken cancellationToken)
    {
        var notifications = await _mediator.Send(
            new GetUnreadNotificationsQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<NotificationDto>>
                .SuccessResponse(
                    notifications,
                    "Unread notifications retrieved successfully."));
    }

    [HttpPut("read/{id:int}")]
    public async Task<IActionResult> MarkAsRead(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new MarkNotificationAsReadCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Notification marked as read."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteNotificationCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Notification deleted successfully."));
    }
}