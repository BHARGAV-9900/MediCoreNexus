using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;
using MediCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;
using MediCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;
using MediCore.Application.Features.MedicalRecords.Queries;
using MediCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;
using MediCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateMedicalRecordCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Medical record created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var records = await _mediator.Send(
            new GetAllMedicalRecordsQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<MedicalRecordDto>>.SuccessResponse(
                records,
                "Medical records retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var record = await _mediator.Send(
            new GetMedicalRecordByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<MedicalRecordDto>.SuccessResponse(
                record,
                "Medical record retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateMedicalRecordCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route Id and Command Id must match.");

        await _mediator.Send(command, cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Medical record updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteMedicalRecordCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "Medical record deleted successfully."));
    }
}