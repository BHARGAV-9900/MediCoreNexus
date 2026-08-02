using MediatR;
using Asp.Versioning;
using MediCore.Application.Common.Pagination;
using MediCore.Application.Features.Patients.Commands.CreatePatient;
using MediCore.Application.Features.Patients.Commands.DeletePatient;
using MediCore.Application.Features.Patients.Commands.UpdatePatient;
using MediCore.Application.Features.Patients.Queries.GetAllPatients;
using MediCore.Application.Features.Patients.Queries.GetPagedPatients;
using MediCore.Application.Features.Patients.Queries.GetPatientById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "PatientManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Patient created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPatientsQuery());

        return Ok(
            ApiResponse<IEnumerable<PatientDto>>
                .SuccessResponse(
                    result,
                    "Patients retrieved successfully."));
    }

    [HttpGet("paged")]
    [ProducesResponseType(
    typeof(ApiResponse<PagedResult<PatientDto>>),
    StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaged(
    [FromQuery] GetPagedPatientsQuery query,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(
            ApiResponse<PagedResult<PatientDto>>.SuccessResponse(
                result,
                "Patients retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetPatientByIdQuery
            {
                Id = id
            });

        return Ok(
            ApiResponse<PatientDto>
                .SuccessResponse(
                    result,
                    "Patient retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePatientCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(
                ApiResponse<object>.FailureResponse(
                    "Route id and request id must match."));
        }

        await _mediator.Send(command);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Patient updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(
            new DeletePatientCommand
            {
                Id = id
            });

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Patient deleted successfully."));
    }
}