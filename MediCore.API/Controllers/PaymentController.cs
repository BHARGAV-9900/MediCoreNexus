using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Billing.Commands.CreatePayment;
using MediCore.Application.Features.Billing.Commands.DeletePayment;
using MediCore.Application.Features.Billing.Commands.UpdatePayment;
using MediCore.Application.Features.Billing.Queries;
using MediCore.Application.Features.Billing.Queries.GetAllPayments;
using MediCore.Application.Features.Billing.Queries.GetPaymentById;
using MediCore.Application.Features.Billing.Queries.GetPaymentsByBill;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[Authorize(Policy = "PaymentManagement")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreatePaymentCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Payment created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetAll()
    {
        var payments = await _mediator.Send(
            new GetAllPaymentsQuery());

        return Ok(ApiResponse<IEnumerable<PaymentDto>>
            .SuccessResponse(
                payments,
                "Payments retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<PaymentDto>>> GetById(int id)
    {
        var payment = await _mediator.Send(
            new GetPaymentByIdQuery(id));

        return Ok(ApiResponse<PaymentDto>
            .SuccessResponse(
                payment,
                "Payment retrieved successfully."));
    }

    [HttpGet("bill/{billId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PaymentDto>>>> GetByBill(
        int billId)
    {
        var payments = await _mediator.Send(
            new GetPaymentsByBillQuery(billId));

        return Ok(ApiResponse<IEnumerable<PaymentDto>>
            .SuccessResponse(
                payments,
                "Payments retrieved successfully."));
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        UpdatePaymentCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Payment updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeletePaymentCommand(id));

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Payment deleted successfully."));
    }
}