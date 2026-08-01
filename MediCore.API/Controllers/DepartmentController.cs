using MediatR;
using MediCore.Application.Features.Departments.Commands.CreateDepartment;
using MediCore.Application.Features.Departments.Commands.DeleteDepartment;
using MediCore.Application.Features.Departments.Commands.UpdateDepartment;
using MediCore.Application.Features.Departments.Queries.GetAllDepartments;
using MediCore.Application.Features.Departments.Queries.GetDepartmentById;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MediCore.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateDepartmentCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ApiResponse<int>.SuccessResponse(
                id,
                "Department created successfully."));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());

        return Ok(
            ApiResponse<IEnumerable<DepartmentDto>>
                .SuccessResponse(
                    result,
                    "Departments retrieved successfully."));
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetDepartmentByIdQuery(id));

        return Ok(
            ApiResponse<DepartmentDto>
                .SuccessResponse(
                    result,
                    "Department retrieved successfully."));
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateDepartmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(
                ApiResponse<object>.FailureResponse(
                    "Route id and request id must match."));
        }

        await _mediator.Send(command);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Department updated successfully."));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(
            new DeleteDepartmentCommand(id));

        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Department deleted successfully."));
    }
}