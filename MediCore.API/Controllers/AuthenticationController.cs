using MediatR;
using MediCore.Application.Features.Authentication.Commands.ChangePassword;
using MediCore.Application.Features.Authentication.Commands.Login;
using MediCore.Application.Features.Authentication.Commands.Logout;
using MediCore.Application.Features.Authentication.Commands.RefreshToken;
using MediCore.Application.Features.Authentication.Commands.Register;
using MediCore.Application.Features.Authentication.DTOs;
using MediCore.Application.Features.Authentication.Queries.GetCurrentUser;
using MediCore.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RegisterResponseDto>>> Register(
        RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(
            ApiResponse<RegisterResponseDto>.SuccessResponse(
                result,
                "User registered successfully."));
    }
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(
    LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(
            ApiResponse<LoginResponseDto>.SuccessResponse(
                result,
                "Login successful."));
    }
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(
    RefreshTokenCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(
            ApiResponse<LoginResponseDto>.SuccessResponse(
                result,
                "Token refreshed successfully."));
    }
    [HttpPost("logout")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout(
    LogoutCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Logout successful."));
    }
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<CurrentUserProfileDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUser(
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetCurrentUserQuery(),
            cancellationToken);

        return Ok(
            ApiResponse<CurrentUserProfileDto>.SuccessResponse(
                result,
                "User profile retrieved successfully."));
    }
    [Authorize]
    [HttpPost("change-password")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Password changed successfully."));
    }
}