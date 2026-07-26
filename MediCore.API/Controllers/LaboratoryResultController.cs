using MediatR;
using MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryResult;
using MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryResult;
using MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryResult;
using MediCore.Application.Features.Laboratory.Queries;
using MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryResults;
using MediCore.Application.Features.Laboratory.Queries.GetLaboratoryResultById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaboratoryResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public LaboratoryResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(CreateLaboratoryResultCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Laboratory result created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<LaboratoryResultDto>>>> GetAll()
    {
        var results = await _mediator.Send(new GetAllLaboratoryResultsQuery());

        return Ok(ApiResponse<IEnumerable<LaboratoryResultDto>>
            .SuccessResponse(
                results,
                "Laboratory results retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<LaboratoryResultDto>>> GetById(int id)
    {
        var result = await _mediator.Send(new GetLaboratoryResultByIdQuery(id));

        return Ok(ApiResponse<LaboratoryResultDto>
            .SuccessResponse(
                result,
                "Laboratory result retrieved successfully."));
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<bool>>> Update(UpdateLaboratoryResultCommand command)
    {
        var updated = await _mediator.Send(command);

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                updated,
                "Laboratory result updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteLaboratoryResultCommand(id));

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                deleted,
                "Laboratory result deleted successfully."));
    }
}