using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Authentication.DTOs;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Authentication.Commands.Register;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasherService _passwordHasher;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponseDto> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        // Check duplicate email
        if (await _userRepository.ExistsByEmailAsync(
            request.Email,
            cancellationToken))
        {
            throw new ConflictException(
                $"User with email '{request.Email}' already exists.");
        }

        // Validate Role
        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {request.RoleId} was not found.");
        }

        // Hash Password
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // Create User
        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            request.RoleId);

        // Save User
        await _userRepository.AddAsync(user, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        // Response
        return new RegisterResponseDto
        {
            PublicId = user.PublicId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = role.Name
        };
    }
}