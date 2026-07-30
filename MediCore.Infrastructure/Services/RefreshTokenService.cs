using System.Security.Cryptography;
using MediCore.Application.Interfaces.Services;

namespace MediCore.Infrastructure.Services;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }
}