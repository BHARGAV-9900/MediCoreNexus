using MediatR;
using Asp.Versioning;
using MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryTest;
using MediCore.Application.Features.Laboratory.Commands.DeleteLaboratoryTest;
using MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryTest;
using MediCore.Application.Features.Laboratory.Queries;
using MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryTests;
using MediCore.Application.Features.Laboratory.Queries.GetLaboratoryTestById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LaboratoryTestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LaboratoryTestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<int>>> Create(
        CreateLaboratoryTestCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Laboratory test created successfully."));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<LaboratoryTestDto>>>> GetAll()
    {
        var tests = await _mediator.Send(
            new GetAllLaboratoryTestsQuery());

        return Ok(ApiResponse<IEnumerable<LaboratoryTestDto>>
            .SuccessResponse(
                tests,
                "Laboratory tests retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<LaboratoryTestDto>>> GetById(int id)
    {
        var test = await _mediator.Send(
            new GetLaboratoryTestByIdQuery(id));

        return Ok(ApiResponse<LaboratoryTestDto>
            .SuccessResponse(
                test,
                "Laboratory test retrieved successfully."));
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        UpdateLaboratoryTestCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Laboratory test updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeleteLaboratoryTestCommand(id));

        return Ok(ApiResponse<bool>
            .SuccessResponse(
                result,
                "Laboratory test deleted successfully."));
    }
}