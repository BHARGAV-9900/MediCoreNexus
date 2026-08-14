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

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("PatientManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));

            options.AddPolicy("AppointmentManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));

            options.AddPolicy("DoctorView", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Receptionist"));

            options.AddPolicy("DoctorManagement", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("DepartmentView", policy =>
                policy.RequireRole(
                    "Admin",
                    "Receptionist"));

            options.AddPolicy("DepartmentManagement", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("MedicalRecordManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor"));

            options.AddPolicy("LaboratoryManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Lab Technician"));

            options.AddPolicy("PharmacyManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Pharmacist"));

            options.AddPolicy("PrescriptionManagement", policy =>
                policy.RequireRole(
                    "Admin",
                    "Doctor",
                    "Pharmacist"));
        });

        return services;
    }
}