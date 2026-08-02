using MediatR;

namespace MediCore.Application.Features.Notification.Queries.GetUnreadNotifications;

public class GetUnreadNotificationsQuery
    : IRequest<IEnumerable<NotificationDto>>
{
}