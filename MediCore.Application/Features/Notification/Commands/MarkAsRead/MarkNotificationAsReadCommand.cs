using MediatR;

namespace MediCore.Application.Features.Notification.Commands.MarkAsRead;

public class MarkNotificationAsReadCommand : IRequest<bool>
{
    public int Id { get; set; }

    public MarkNotificationAsReadCommand(int id)
    {
        Id = id;
    }
}