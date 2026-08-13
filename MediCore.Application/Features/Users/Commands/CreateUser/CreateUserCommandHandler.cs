using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Users.DTOs;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasherService _passwordHasher;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(
            request.Email,
            cancellationToken))
        {
            throw new ConflictException(
                $"User with email '{request.Email}' already exists.");
        }

        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {request.RoleId} was not found.");
        }

        var passwordHash =
            _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            request.RoleId);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

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