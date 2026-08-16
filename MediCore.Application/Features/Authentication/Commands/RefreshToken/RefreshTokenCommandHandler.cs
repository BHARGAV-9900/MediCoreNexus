using MediatR;

using MediCore.Application.Exceptions;
using MediCore.Application.Features.Authentication.DTOs;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

using RefreshTokenEntity =
    MediCore.Domain.Entities.RefreshToken;

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
        _refreshTokenRepository =
            refreshTokenRepository;

        _jwtTokenService =
            jwtTokenService;

        _refreshTokenService =
            refreshTokenService;
    }


    public async Task<LoginResponseDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        // =========================================================
        // GET REFRESH TOKEN
        // =========================================================

        var existingToken =
            await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken,
                cancellationToken);


        // =========================================================
        // TOKEN EXISTENCE
        // =========================================================

        if (existingToken is null)
        {
            throw new UnauthorizedException(
                "Invalid refresh token.");
        }


        // =========================================================
        // TOKEN REVOCATION
        // =========================================================

        if (existingToken.IsRevoked)
        {
            throw new UnauthorizedException(
                "Refresh token has been revoked.");
        }


        // =========================================================
        // TOKEN EXPIRATION
        // =========================================================

        if (existingToken.ExpiresOn <= DateTime.UtcNow)
        {
            throw new UnauthorizedException(
                "Refresh token has expired.");
        }


        // =========================================================
        // USER EXISTENCE
        // =========================================================

        if (existingToken.User is null)
        {
            throw new UnauthorizedException(
                "User not found.");
        }


        // =========================================================
        // USER ACTIVE STATUS
        // =========================================================

        /*
         * IMPORTANT:
         *
         * A refresh token may still be valid even after the
         * administrator deactivates the user.
         *
         * Therefore we MUST check the current database state
         * before generating a new access token.
         */

        if (!existingToken.User.IsActive)
        {
            throw new UnauthorizedException(
                "User account is inactive.");
        }


        // =========================================================
        // REVOKE OLD REFRESH TOKEN
        // =========================================================

        existingToken.Revoke();


        // =========================================================
        // GENERATE NEW ACCESS TOKEN
        // =========================================================

        var accessToken =
            _jwtTokenService.GenerateToken(
                existingToken.User);


        // =========================================================
        // GENERATE NEW REFRESH TOKEN
        // =========================================================

        var newRefreshTokenValue =
            _refreshTokenService.GenerateRefreshToken();


        var newRefreshToken =
            new RefreshTokenEntity(
                existingToken.User.Id,
                newRefreshTokenValue,
                DateTime.UtcNow.AddDays(7));


        await _refreshTokenRepository.AddAsync(
            newRefreshToken,
            cancellationToken);


        // =========================================================
        // SAVE CHANGES
        // =========================================================

        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);


        // =========================================================
        // RETURN NEW TOKEN PAIR
        // =========================================================

        return new LoginResponseDto
        {
            AccessToken =
                accessToken,

            RefreshToken =
                newRefreshTokenValue,

            ExpiresAt =
                _jwtTokenService.GetTokenExpiry()
        };
    }
}