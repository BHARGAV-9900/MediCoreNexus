using MediatR;
using MediCore.Application.Features.PrescriptionItems.Commands.CreatePrescriptionItem;
using MediCore.Application.Features.PrescriptionItems.Commands.DeletePrescriptionItem;
using MediCore.Application.Features.PrescriptionItems.Commands.UpdatePrescriptionItem;
using MediCore.Application.Features.PrescriptionItems.Queries;
using MediCore.Application.Features.PrescriptionItems.Queries.GetAllPrescriptionItems;
using MediCore.Application.Features.PrescriptionItems.Queries.GetPrescriptionItemById;
using MediCore.Application.Features.PrescriptionItems.Queries.GetPrescriptionItemsByPrescription;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreatePrescriptionItemCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Prescription item created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PrescriptionItemDto>>>> GetAll()
    {
        var items = await _mediator.Send(
            new GetAllPrescriptionItemsQuery());

        return Ok(
            ApiResponse<IEnumerable<PrescriptionItemDto>>
                .SuccessResponse(
                    items,
                    "Prescription items retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<PrescriptionItemDto>>> GetById(
        int id)
    {
        var item = await _mediator.Send(
            new GetPrescriptionItemByIdQuery(id));

        return Ok(
            ApiResponse<PrescriptionItemDto>
                .SuccessResponse(
                    item,
                    "Prescription item retrieved successfully."));
    }

    [HttpGet("prescription/{prescriptionId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PrescriptionItemDto>>>> GetByPrescription(
    int prescriptionId)
    {
        var result = await _mediator.Send(
            new GetPrescriptionItemsByPrescriptionQuery(prescriptionId));

        return Ok(
            ApiResponse<IEnumerable<PrescriptionItemDto>>
                .SuccessResponse(
                    result,
                    "Prescription items retrieved successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        int id,
        UpdatePrescriptionItemCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Prescription item updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeletePrescriptionItemCommand(id));

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Prescription item deleted successfully."));
    }
}