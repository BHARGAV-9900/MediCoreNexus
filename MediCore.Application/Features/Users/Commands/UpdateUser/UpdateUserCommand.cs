using MediatR;
using MediCore.Application.Features.Users.DTOs;

namespace MediCore.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<UserDto>
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int RoleId { get; set; }
}