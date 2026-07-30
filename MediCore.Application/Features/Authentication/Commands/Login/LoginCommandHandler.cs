using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Authentication.DTOs;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using RefreshTokenEntity = MediCore.Domain.Entities.RefreshToken;

namespace MediCore.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasherService passwordHasher,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<LoginResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (user is null)
            throw new UnauthorizedException("Invalid email or password.");

        if (!_passwordHasher.VerifyPassword(
            user.PasswordHash,
            request.Password))
        {
            throw new UnauthorizedException("Invalid email or password.");
        }

        if (!user.IsActive)
            throw new UnauthorizedException("User account is inactive.");

        var accessToken = _jwtTokenService.GenerateToken(user);

        var refreshTokenValue =
            _refreshTokenService.GenerateRefreshToken();

        var refreshToken = new RefreshTokenEntity(
            user.Id,
            refreshTokenValue,
            DateTime.UtcNow.AddDays(7));

        await _refreshTokenRepository.AddAsync(
            refreshToken,
            cancellationToken);

        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            ExpiresAt = _jwtTokenService.GetTokenExpiry()
        };
    }
}