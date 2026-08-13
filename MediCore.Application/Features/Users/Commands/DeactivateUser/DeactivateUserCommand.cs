using MediatR;

namespace MediCore.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeactivateUserCommand(int id)
    {
        Id = id;
    }
}