using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Commands.ActivateUser;

public class ActivateUserCommandHandler
    : IRequestHandler<ActivateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ActivateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(
        ActivateUserCommand request,
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

        user.Activate();

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}