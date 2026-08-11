using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetAllNotifications;

public class GetAllNotificationsQueryHandler
    : IRequestHandler<
        GetAllNotificationsQuery,
        IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;

    public GetAllNotificationsQueryHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(
        GetAllNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications =
            await _repository.GetAllAsync(
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