using MediatR;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Notification.Queries.GetAllNotifications;

public class GetAllNotificationsQueryHandler
    : IRequestHandler<
        GetAllNotificationsQuery,
        IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetAllNotificationsQueryHandler(
        INotificationRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(
        GetAllNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated ||
            !_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }

        var notifications =
            await _repository.GetAllAsync(
                _currentUserService.UserId.Value,
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