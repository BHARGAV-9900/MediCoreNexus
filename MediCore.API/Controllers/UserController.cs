using Asp.Versioning;
using MediatR;
using MediCore.Application.Features.Users.Commands.CreateUser;
using MediCore.Application.Features.Users.Queries.GetUserById;
using MediCore.Application.Features.Users.Queries.GetUsers;
using MediCore.Application.Features.Users.Commands.UpdateUser;
using MediCore.Application.Features.Users.Commands.ActivateUser;
using MediCore.Application.Features.Users.Commands.DeactivateUser;
using MediCore.Application.Features.Users.Commands.DeleteUser;
using MediCore.Application.Features.Users.DTOs;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(ApiResponse<IEnumerable<UserDto>>),
        StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUsersQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<IEnumerable<UserDto>>.SuccessResponse(
                result,
                "Users retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(ApiResponse<UserDto>),
        StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(
            ApiResponse<UserDto>.SuccessResponse(
                result,
                "User retrieved successfully."));
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(ApiResponse<UserDto>),
        StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<UserDto>.SuccessResponse(
                result,
                "User created successfully."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateUserCommand command,
    CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<UserDto>.SuccessResponse(
                result,
                "User updated successfully."));
    }

    [HttpPatch("{id:int}/activate")]
    public async Task<IActionResult> Activate(
    int id,
    CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new ActivateUserCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "User activated successfully."));
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> Deactivate(
    int id,
    CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeactivateUserCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "User deactivated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteUserCommand(id),
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                true,
                "User deleted successfully."));
    }
}