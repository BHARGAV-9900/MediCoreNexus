using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetUnreadNotifications;

public class GetUnreadNotificationsQueryHandler
    : IRequestHandler<
        GetUnreadNotificationsQuery,
        IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;

    public GetUnreadNotificationsQueryHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(
        GetUnreadNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications =
            await _repository.GetUnreadAsync(
                cancellationToken);

        return notifications.Select(
            notification => new NotificationDto
            {
                Id = notification.Id,

                UserId = notification.UserId,

                Title = notification.Title,

                Message = notification.Message,

                Type = notification.Type,

                IsRead = notification.IsRead,

                ReadAt = notification.ReadAt,

                CreatedAt = notification.CreatedAt
            });
    }
}