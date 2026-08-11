using MediatR;

namespace MediCore.Application.Features.Notification.Commands.CreateNotification;

public class CreateNotificationCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}