using MediatR;
using MediCore.Application.Interfaces.Repositories;

using NotificationEntity = MediCore.Domain.Entities.Notification;

namespace MediCore.Application.Features.Notification.Commands.CreateNotification;

public class CreateNotificationCommandHandler
    : IRequestHandler<CreateNotificationCommand, int>
{
    private readonly INotificationRepository _notificationRepository;

    public CreateNotificationCommandHandler(
        INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<int> Handle(
        CreateNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var notification = new NotificationEntity(
            request.UserId,
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