using Asp.Versioning;
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
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // ============================================================
    // REGISTER
    // ============================================================
    // Only an Administrator should be able to create hospital
    // staff accounts.
    //
    // Normal staff/users must NOT be able to register themselves.
    // ============================================================

    [Authorize(Policy = "AdminOnly")]
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


    // ============================================================
    // LOGIN
    // ============================================================
    // Login must remain public.
    // ============================================================

    [AllowAnonymous]
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


    // ============================================================
    // REFRESH TOKEN
    // ============================================================
    // Refresh token must remain accessible without an access token.
    // The RefreshTokenCommandHandler is responsible for validating
    // the refresh token and current user status.
    // ============================================================

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(
        typeof(ApiResponse<LoginResponseDto>),
        StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<LoginResponseDto>.SuccessResponse(
                result,
                "Token refreshed successfully."));
    }


    // ============================================================
    // LOGOUT
    // ============================================================

    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(
        typeof(ApiResponse<bool>),
        StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout(
        LogoutCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(
            ApiResponse<bool>.SuccessResponse(
                result,
                "Logout successful."));
    }


    // ============================================================
    // CURRENT USER
    // ============================================================

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(
        typeof(ApiResponse<CurrentUserProfileDto>),
        StatusCodes.Status200OK)]
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


    // ============================================================
    // CHANGE PASSWORD
    // ============================================================

    [Authorize]
    [HttpPost("change-password")]
    [ProducesResponseType(
        typeof(ApiResponse<bool>),
        StatusCodes.Status200OK)]
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