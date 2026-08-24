using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Application.Features.Notification.Commands.DeleteNotification;

public class DeleteNotificationCommandHandler
    : IRequestHandler<DeleteNotificationCommand, bool>
{
    private readonly INotificationRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteNotificationCommandHandler(
        INotificationRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(
        DeleteNotificationCommand request,
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
            throw new NotFoundException("Notification not found.");

        notification.Delete();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}