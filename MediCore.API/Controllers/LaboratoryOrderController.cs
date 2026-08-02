using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryOrder;
using MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryOrder;
using MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryOrder;
using MediCore.Application.Features.Laboratory.Queries;
using MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryOrders;
using MediCore.Application.Features.Laboratory.Queries.GetLaboratoryOrderById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LaboratoryOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public LaboratoryOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(CreateLaboratoryOrderCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Laboratory order created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<LaboratoryOrderDto>>>> GetAll()
    {
        var orders = await _mediator.Send(new GetAllLaboratoryOrdersQuery());

        return Ok(ApiResponse<IEnumerable<LaboratoryOrderDto>>
            .SuccessResponse(
                orders,
                "Laboratory orders retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<LaboratoryOrderDto>>> GetById(int id)
    {
        var order = await _mediator.Send(new GetLaboratoryOrderByIdQuery(id));

        return Ok(ApiResponse<LaboratoryOrderDto>
            .SuccessResponse(
                order,
                "Laboratory order retrieved successfully."));
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<bool>>> Update(UpdateLaboratoryOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Laboratory order updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteLaboratoryOrderCommand(id));

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Laboratory order deleted successfully."));
    }
}