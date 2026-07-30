using MediatR;

namespace MediCore.Application.Features.Authentication.Commands.Logout;

public class LogoutCommand : IRequest<bool>
{
    public string RefreshToken { get; set; } = string.Empty;
}