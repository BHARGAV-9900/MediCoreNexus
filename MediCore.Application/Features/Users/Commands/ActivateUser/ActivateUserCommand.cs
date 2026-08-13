using MediatR;

namespace MediCore.Application.Features.Users.Commands.ActivateUser;

public class ActivateUserCommand : IRequest<bool>
{
    public int Id { get; set; }

    public ActivateUserCommand(int id)
    {
        Id = id;
    }
}