using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class Notification : BaseAuditableEntity
{
    private Notification()
    {
    }

    public Notification(
        Guid userId,
        string title,
        string message,
        string type)
    {
        UserId = userId;
        SetTitle(title);
        SetMessage(message);

        Type = type;
        IsRead = false;
    }

    public Guid UserId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public string Type { get; private set; } = string.Empty;

    public bool IsRead { get; private set; }

    public DateTime? ReadAt { get; private set; }

    public void MarkAsRead()
    {
        if (IsRead)
            return;

        IsRead = true;
        ReadAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string title,
        string message)
    {
        SetTitle(title);
        SetMessage(message);

        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.");

        Title = title.Trim();
    }

    private void SetMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message is required.");

        Message = message.Trim();
    }
}