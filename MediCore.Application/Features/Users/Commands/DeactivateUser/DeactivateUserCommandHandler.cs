using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommandHandler
    : IRequestHandler<DeactivateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeactivateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(
        DeactivateUserCommand request,
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

        user.Deactivate();

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}