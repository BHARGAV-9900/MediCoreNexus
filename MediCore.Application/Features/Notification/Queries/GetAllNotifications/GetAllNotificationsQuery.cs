using MediatR;

namespace MediCore.Application.Features.Notification.Queries.GetAllNotifications;

public class GetAllNotificationsQuery
    : IRequest<IEnumerable<NotificationDto>>
{
}