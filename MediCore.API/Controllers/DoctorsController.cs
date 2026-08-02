using MediatR;
using Asp.Versioning;
using MediCore.Application.Features.Doctors.Commands.CreateDoctor;
using MediCore.Application.Features.Doctors.Commands.DeleteDoctor;
using MediCore.Application.Features.Doctors.Commands.UpdateDoctor;
using MediCore.Application.Features.Doctors.DTOs;
using MediCore.Application.Features.Doctors.Queries.GetAllDoctors;
using MediCore.Application.Features.Doctors.Queries.GetDoctorById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "DoctorManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreateDoctorCommand command)
    {
        var doctorId = await _mediator.Send(command);

        return Ok(ApiResponse<int>.SuccessResponse(
            doctorId,
            "Doctor created successfully."));
    }
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<DoctorDto>>>> GetAll()
    {
        var doctors = await _mediator.Send(new GetAllDoctorsQuery());

        return Ok(ApiResponse<IEnumerable<DoctorDto>>.SuccessResponse(
            doctors,
            "Doctors retrieved successfully."));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<DoctorDto>>> GetById(int id)
    {
        var doctor = await _mediator.Send(new GetDoctorByIdQuery(id));

        return Ok(ApiResponse<DoctorDto>.SuccessResponse(
            doctor,
            "Doctor retrieved successfully."));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(
    int id,
    UpdateDoctorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object>.FailureResponse(
                "Route Id and Request Id do not match."));
        }

        await _mediator.Send(command);

        return Ok(ApiResponse<bool>.SuccessResponse(
            true,
            "Doctor updated successfully."));
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        await _mediator.Send(new DeleteDoctorCommand(id));

        return Ok(ApiResponse<bool>.SuccessResponse(
            true,
            "Doctor deleted successfully."));
    }
}