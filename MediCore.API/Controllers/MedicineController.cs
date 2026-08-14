using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Medicines.Commands.CreateMedicine;
using MediCore.Application.Features.Medicines.Commands.DeleteMedicine;
using MediCore.Application.Features.Medicines.Commands.UpdateMedicine;
using MediCore.Application.Features.Medicines.Queries;
using MediCore.Application.Features.Medicines.Queries.GetAllMedicines;
using MediCore.Application.Features.Medicines.Queries.GetMedicineById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MediCore.API.Controllers;
[Authorize(Policy = "PharmacyManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MedicineController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateMedicineCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Medicine created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var medicines = await _mediator.Send(
            new GetAllMedicinesQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<MedicineDto>>.SuccessResponse(
                medicines,
                "Medicines retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var medicine = await _mediator.Send(
            new GetMedicineByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<MedicineDto>.SuccessResponse(
                medicine,
                "Medicine retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateMedicineCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Medicine updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteMedicineCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Medicine deleted successfully."));
    }
}