using MediatR;
using MediCore.Application.Features.Authentication.DTOs;

namespace MediCore.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<LoginResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}