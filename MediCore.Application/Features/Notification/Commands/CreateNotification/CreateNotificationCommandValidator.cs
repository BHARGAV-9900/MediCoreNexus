using FluentValidation;

namespace MediCore.Application.Features.Notification.Commands.CreateNotification;

public class CreateNotificationCommandValidator
    : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Type)
            .NotEmpty()
            .MaximumLength(50);
    }
}