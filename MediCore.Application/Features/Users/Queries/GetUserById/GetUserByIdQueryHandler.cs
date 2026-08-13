using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Users.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(
                $"User with Id {request.Id} was not found.");
        }

        return new UserDto
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
        };
    }
}