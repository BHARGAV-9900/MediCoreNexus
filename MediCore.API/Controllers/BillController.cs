using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Billing.Commands.CreateBill;
using MediCore.Application.Features.Billing.Commands.DeleteBill;
using MediCore.Application.Features.Billing.Commands.UpdateBill;
using MediCore.Application.Features.Billing.Queries;
using MediCore.Application.Features.Billing.Queries.GetAllBills;
using MediCore.Application.Features.Billing.Queries.GetBillById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BillController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreateBillCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Bill created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<BillDto>>>> GetAll()
    {
        var bills = await _mediator.Send(
            new GetAllBillsQuery());

        return Ok(
            ApiResponse<IEnumerable<BillDto>>
                .SuccessResponse(
                    bills,
                    "Bills retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<BillDto>>> GetById(
        int id)
    {
        var bill = await _mediator.Send(
            new GetBillByIdQuery(id));

        return Ok(
            ApiResponse<BillDto>
                .SuccessResponse(
                    bill,
                    "Bill retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        int id,
        UpdateBillCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Bill updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(
        int id)
    {
        var result = await _mediator.Send(
            new DeleteBillCommand(id));

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Bill deleted successfully."));
    }
}