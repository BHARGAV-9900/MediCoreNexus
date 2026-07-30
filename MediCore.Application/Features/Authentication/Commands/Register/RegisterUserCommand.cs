using MediatR;
using MediCore.Application.Features.Authentication.DTOs;

namespace MediCore.Application.Features.Authentication.Commands.Register;

public class RegisterUserCommand : IRequest<RegisterResponseDto>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }
}