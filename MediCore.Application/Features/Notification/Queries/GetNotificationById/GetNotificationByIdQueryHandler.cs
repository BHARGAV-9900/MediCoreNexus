using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Notification.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler
    : IRequestHandler<
        GetNotificationByIdQuery,
        NotificationDto>
{
    private readonly INotificationRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetNotificationByIdQueryHandler(
        INotificationRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<NotificationDto> Handle(
        GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated ||
            !_currentUserService.UserId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }

        var notification =
            await _repository.GetByIdAsync(
                request.Id,
                _currentUserService.UserId.Value,
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