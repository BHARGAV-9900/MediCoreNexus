using MediatR;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

using NotificationEntity = MediCore.Domain.Entities.Notification;

namespace MediCore.Application.Features.Notification.Commands.CreateNotification;

public class CreateNotificationCommandHandler
    : IRequestHandler<CreateNotificationCommand, int>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateNotificationCommandHandler(
        INotificationRepository notificationRepository,
        ICurrentUserService currentUserService)
    {
        _notificationRepository = notificationRepository;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(
        CreateNotificationCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }

        if (!_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "User ID could not be determined.");
        }

        var userId = _currentUserService.UserId.Value;

        var notification = new NotificationEntity(
            userId,
            request.Title,
            request.Message,
            request.Type);

        await _notificationRepository.AddAsync(
            notification,
            cancellationToken);

        await _notificationRepository.SaveChangesAsync(
            cancellationToken);

        return notification.Id;
    }
}