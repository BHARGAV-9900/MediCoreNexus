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

    public int? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return int.TryParse(userId, out var id)
                ? id
                : null;
        }
    }

    public string? Email =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Email)?
            .Value;

    public string? Role =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Role)?
            .Value;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? false;
}