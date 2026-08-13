using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Users.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDto> Handle(
        UpdateUserCommand request,
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

        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {request.RoleId} was not found.");
        }

        var existingUser = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (existingUser is not null &&
            existingUser.Id != request.Id)
        {
            throw new ConflictException(
                $"User with email '{request.Email}' already exists.");
        }

        user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.RoleId);

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            PublicId = user.PublicId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Email = user.Email,
            RoleId = user.RoleId,
            Role = role.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}