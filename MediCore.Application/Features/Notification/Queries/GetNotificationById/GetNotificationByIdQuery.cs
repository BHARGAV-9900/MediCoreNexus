using MediatR;

namespace MediCore.Application.Features.Notification.Queries.GetNotificationById;

public class GetNotificationByIdQuery : IRequest<NotificationDto>
{
    public int Id { get; }

    public GetNotificationByIdQuery(int id)
    {
        Id = id;
    }
}