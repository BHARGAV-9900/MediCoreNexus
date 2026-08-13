using MediatR;
using MediCore.Application.Features.Users.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return users.Select(user => new UserDto
        {
            Id = user.Id,
            PublicId = user.PublicId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Email = user.Email,
            RoleId = user.RoleId,
            Role = user.Role?.Name ?? string.Empty,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        });
    }
}