using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(
        DeleteUserCommand request,
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

        user.Delete();

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}