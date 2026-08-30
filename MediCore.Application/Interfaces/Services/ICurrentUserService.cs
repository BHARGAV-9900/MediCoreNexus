namespace MediCore.Application.Interfaces.Services;

public interface ICurrentUserService
{
    int? UserId { get; }

    string? Email { get; }

    string? Role { get; }

    string? IpAddress { get; }

    string? RequestPath { get; }

    string? RequestId { get; }

    bool IsAuthenticated { get; }
}
