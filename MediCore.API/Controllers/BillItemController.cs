using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Billing.Commands.CreateBillItem;
using MediCore.Application.Features.Billing.Commands.DeleteBillItem;
using MediCore.Application.Features.Billing.Commands.UpdateBillItem;
using MediCore.Application.Features.Billing.Queries;
using MediCore.Application.Features.Billing.Queries.GetAllBillItems;
using MediCore.Application.Features.Billing.Queries.GetBillItemsByBill;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "BillingManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BillItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(CreateBillItemCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<int>.SuccessResponse(
            id,
            "Bill item created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<BillItemDto>>>> GetAll()
    {
        var items = await _mediator.Send(new GetAllBillItemsQuery());

        return Ok(ApiResponse<IEnumerable<BillItemDto>>.SuccessResponse(
            items,
            "Bill items retrieved successfully."));
    }

    [HttpGet("bill/{billId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<BillItemDto>>>> GetByBill(int billId)
    {
        var items = await _mediator.Send(new GetBillItemsByBillQuery(billId));

        return Ok(ApiResponse<IEnumerable<BillItemDto>>.SuccessResponse(
            items,
            "Bill items retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        int id,
        UpdateBillItemCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<bool>.SuccessResponse(
            result,
            "Bill item updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBillItemCommand(id));

        return Ok(ApiResponse<bool>.SuccessResponse(
            result,
            "Bill item deleted successfully."));
    }
}