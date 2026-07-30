namespace MediCore.Application.Interfaces.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
}