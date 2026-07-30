using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, CurrentUserProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CurrentUserProfileDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated ||
            !_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedException("User is not authenticated.");
        }

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId.Value,
            cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found.");
        }

        return new CurrentUserProfileDto
        {
            PublicId = user.PublicId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role?.Name ?? string.Empty
        };
    }
}