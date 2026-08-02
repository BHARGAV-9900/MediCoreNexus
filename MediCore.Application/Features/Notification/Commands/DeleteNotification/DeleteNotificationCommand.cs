using MediatR;

namespace MediCore.Application.Features.Notification.Commands.DeleteNotification;

public class DeleteNotificationCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteNotificationCommand(int id)
    {
        Id = id;
    }
}