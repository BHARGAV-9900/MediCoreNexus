using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Prescriptions.Commands.CreatePrescription;
using MediCore.Application.Features.Prescriptions.Commands.DeletePrescription;
using MediCore.Application.Features.Prescriptions.Commands.UpdatePrescription;
using MediCore.Application.Features.Prescriptions.Queries;
using MediCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;
using MediCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreatePrescriptionCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Prescription created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PrescriptionDto>>>> GetAll()
    {
        var prescriptions = await _mediator.Send(
            new GetAllPrescriptionsQuery());

        return Ok(
            ApiResponse<IEnumerable<PrescriptionDto>>
                .SuccessResponse(
                    prescriptions,
                    "Prescriptions retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<PrescriptionDto>>> GetById(int id)
    {
        var prescription = await _mediator.Send(
            new GetPrescriptionByIdQuery(id));

        return Ok(
            ApiResponse<PrescriptionDto>
                .SuccessResponse(
                    prescription,
                    "Prescription retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        int id,
        UpdatePrescriptionCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Prescription updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeletePrescriptionCommand(id));

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Prescription deleted successfully."));
    }
}