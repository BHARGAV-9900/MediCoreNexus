using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPasswordHasherService _passwordHasher;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        // Check authentication
        if (!_currentUserService.IsAuthenticated ||
            !_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedException("User is not authenticated.");
        }

        // Get logged-in user
        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId.Value,
            cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found.");
        }

        // Verify current password
        var isPasswordValid = _passwordHasher.VerifyPassword(
            user.PasswordHash,
            request.CurrentPassword);

        if (!isPasswordValid)
        {
            throw new UnauthorizedException("Current password is incorrect.");
        }

        // Hash new password
        var newPasswordHash = _passwordHasher.HashPassword(
            request.NewPassword);

        // Update password
        user.ChangePassword(newPasswordHash);

        // Save changes
        await _userRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}