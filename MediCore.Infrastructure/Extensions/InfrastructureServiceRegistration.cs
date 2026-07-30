using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediCore.Application.Interfaces.Services;
using MediCore.Infrastructure.Authentication;
using MediCore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MediCore.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Services here
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.Configure<JwtSettings>(
                configuration.GetSection(JwtSettings.SectionName));
        var jwtSettings = configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;

        services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("==================================");
                Console.WriteLine("JWT AUTHENTICATION FAILED");
                Console.WriteLine(context.Exception);
                Console.WriteLine("==================================");
                return Task.CompletedTask;
            }
        };
    });

        services.AddAuthorization();

        return services;
    }
}