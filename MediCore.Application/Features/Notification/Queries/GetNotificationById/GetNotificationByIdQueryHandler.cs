using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Notification.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler
    : IRequestHandler<
        GetNotificationByIdQuery,
        NotificationDto>
{
    private readonly INotificationRepository _repository;

    public GetNotificationByIdQueryHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationDto> Handle(
        GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var notification =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (notification is null)
        {
            throw new NotFoundException(
                "Notification not found.");
        }

        return new NotificationDto
        {
            Id = notification.Id,

            UserId = notification.UserId,

            Title = notification.Title,

            Message = notification.Message,

            Type = notification.Type,

            IsRead = notification.IsRead,

            ReadAt = notification.ReadAt,

            CreatedAt = notification.CreatedAt
        };
    }
}