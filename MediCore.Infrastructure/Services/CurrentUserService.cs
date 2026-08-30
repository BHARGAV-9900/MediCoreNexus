using System.Security.Claims;
using MediCore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace MediCore.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

    public int? UserId
    {
        get
        {
            var userId = HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return int.TryParse(userId, out var id)
                ? id
                : null;
        }
    }

    public string? Email =>
        HttpContext?
            .User?
            .FindFirst(ClaimTypes.Email)?
            .Value;

    public string? Role =>
        HttpContext?
            .User?
            .FindFirst(ClaimTypes.Role)?
            .Value;

    public string? IpAddress =>
        HttpContext?.Connection.RemoteIpAddress?.ToString();

    public string? RequestPath =>
        HttpContext?.Request.Path.Value;

    public string? RequestId =>
        HttpContext?.TraceIdentifier;

    public bool IsAuthenticated =>
        HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? false;
}
