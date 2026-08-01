using MediatR;

namespace MediCore.Application.Features.Authentication.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<bool>
{
    public string CurrentPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}