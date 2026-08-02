using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Inventory;
using MediCore.Application.Features.Inventory.Commands.CreateInventory;
using MediCore.Application.Features.Inventory.Commands.DeleteInventory;
using MediCore.Application.Features.Inventory.Commands.UpdateInventory;
using MediCore.Application.Features.Inventory.Queries.GetAllInventory;
using MediCore.Application.Features.Inventory.Queries.GetExpiringInventory;
using MediCore.Application.Features.Inventory.Queries.GetInventoryById;
using MediCore.Application.Features.Inventory.Queries.GetLowStockInventory;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateInventoryCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Inventory created successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var inventory = await _mediator.Send(
            new GetAllInventoryQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<InventoryDto>>
                .SuccessResponse(
                    inventory,
                    "Inventory retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var inventory = await _mediator.Send(
            new GetInventoryByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<InventoryDto>.SuccessResponse(
                inventory,
                "Inventory retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateInventoryCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Inventory updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteInventoryCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Inventory deleted successfully."));
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetLowStockInventoryQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<InventoryDto>>
                .SuccessResponse(
                    result,
                    "Low stock inventory retrieved successfully."));
    }

    [HttpGet("expiring")]
    public async Task<IActionResult> GetExpiring(
        [FromQuery] int days = 30,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetExpiringInventoryQuery(days),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<InventoryDto>>
                .SuccessResponse(
                    result,
                    "Expiring inventory retrieved successfully."));
    }
}