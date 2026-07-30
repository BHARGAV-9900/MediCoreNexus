using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutCommandHandler(
        IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<bool> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(
            request.RefreshToken,
            cancellationToken);

        if (refreshToken is null)
        {
            throw new UnauthorizedException("Invalid refresh token.");
        }

        if (refreshToken.IsRevoked)
        {
            throw new UnauthorizedException("Refresh token has already been revoked.");
        }

        if (refreshToken.ExpiresOn <= DateTime.UtcNow)
        {
            throw new UnauthorizedException("Refresh token has expired.");
        }

        refreshToken.Revoke();

        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}