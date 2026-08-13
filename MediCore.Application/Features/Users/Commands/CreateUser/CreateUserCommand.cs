using MediatR;
using MediCore.Application.Features.Users.DTOs;

namespace MediCore.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<UserDto>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }
}