using MediCore.Domain.Entities;

namespace MediCore.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);

    DateTime GetTokenExpiry();
}