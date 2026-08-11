using MediatR;
using Asp.Versioning;

using MediCore.Application.Features.Appointments.Commands.CreateAppointment;
using MediCore.Application.Features.Appointments.Commands.DeleteAppointment;
using MediCore.Application.Features.Appointments.Commands.UpdateAppointment;
using MediCore.Application.Features.Appointments.Commands.UpdateAppointmentStatus;

using MediCore.Application.Features.Appointments.Queries;
using MediCore.Application.Features.Appointments.Queries.GetAllAppointments;
using MediCore.Application.Features.Appointments.Queries.GetAppointmentById;

using MediCore.Domain.Enums;

using MediCore.Shared.Responses;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "AppointmentManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        var appointmentId =
            await _mediator.Send(
                command,
                cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = appointmentId },
            ApiResponse<int>.SuccessResponse(
                appointmentId,
                "Appointment created successfully."));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var appointments =
            await _mediator.Send(
                new GetAllAppointmentsQuery(),
                cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<AppointmentDto>>
                .SuccessResponse(
                    appointments,
                    "Appointments retrieved successfully."));
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var appointment =
            await _mediator.Send(
                new GetAppointmentByIdQuery(id),
                cancellationToken);

        return Ok(
            ApiResponse<AppointmentDto>
                .SuccessResponse(
                    appointment,
                    "Appointment retrieved successfully."));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(
                ApiResponse<object>.FailureResponse(
                    "Route id and request id must match."));
        }

        await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Appointment updated successfully."));
    }


    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] AppointmentStatus status,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateAppointmentStatusCommand(
                id,
                status),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Appointment status updated successfully."));
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteAppointmentCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Appointment deleted successfully."));
    }
}