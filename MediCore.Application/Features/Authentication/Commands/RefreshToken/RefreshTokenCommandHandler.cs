using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Authentication.DTOs;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using RefreshTokenEntity = MediCore.Domain.Entities.RefreshToken;

namespace MediCore.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<LoginResponseDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(
            request.RefreshToken,
            cancellationToken);

        if (existingToken is null)
        {
            throw new UnauthorizedException("Invalid refresh token.");
        }

        if (existingToken.IsRevoked)
        {
            throw new UnauthorizedException("Refresh token has been revoked.");
        }

        if (existingToken.ExpiresOn <= DateTime.UtcNow)
        {
            throw new UnauthorizedException("Refresh token has expired.");
        }

        if (existingToken.User is null)
        {
            throw new UnauthorizedException("User not found.");
        }

        // Revoke old refresh token
        existingToken.Revoke();

        // Generate new access token
        var accessToken =
            _jwtTokenService.GenerateToken(existingToken.User);

        // Generate new refresh token
        var newRefreshTokenValue =
            _refreshTokenService.GenerateRefreshToken();

        var newRefreshToken = new RefreshTokenEntity(
            existingToken.User.Id,
            newRefreshTokenValue,
            DateTime.UtcNow.AddDays(7));

        await _refreshTokenRepository.AddAsync(
            newRefreshToken,
            cancellationToken);

        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshTokenValue,
            ExpiresAt = _jwtTokenService.GetTokenExpiry()
        };
    }
}