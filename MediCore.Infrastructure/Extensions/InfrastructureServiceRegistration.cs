using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MediCore.Application.Interfaces.Repositories;
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
        // =========================================================
        // Application Services
        // =========================================================

        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();


        // =========================================================
        // JWT Configuration
        // =========================================================

        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        var jwtSettings = configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;


        // =========================================================
        // Authentication
        // =========================================================

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,

                        ValidateAudience = true,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.Issuer,

                        ValidAudience = jwtSettings.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtSettings.SecretKey)),

                        ClockSkew = TimeSpan.Zero
                    };


                // =====================================================
                // JWT EVENTS
                // =====================================================

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine(
                            "==================================");

                        Console.WriteLine(
                            "JWT AUTHENTICATION FAILED");

                        Console.WriteLine(
                            context.Exception);

                        Console.WriteLine(
                            "==================================");

                        return Task.CompletedTask;
                    },


                    // =================================================
                    // IMPORTANT:
                    //
                    // Check the user's CURRENT database status
                    // whenever an existing JWT is presented.
                    //
                    // This prevents a deactivated user from
                    // continuing to use an already-issued JWT.
                    // =================================================

                    OnTokenValidated = async context =>
                    {
                        try
                        {
                            var userRepository =
                                context.HttpContext
                                    .RequestServices
                                    .GetRequiredService<IUserRepository>();


                            // -----------------------------------------
                            // Get email from JWT
                            // -----------------------------------------

                            var email =
                                context.Principal?
                                    .FindFirst(ClaimTypes.Email)
                                    ?.Value
                                ??
                                context.Principal?
                                    .FindFirst(JwtRegisteredClaimNames.Email)
                                    ?.Value;


                            if (string.IsNullOrWhiteSpace(email))
                            {
                                context.Fail(
                                    "User identity could not be determined.");

                                return;
                            }


                            // -----------------------------------------
                            // Get CURRENT user from database
                            // -----------------------------------------

                            var user =
                                await userRepository.GetByEmailAsync(
                                    email,
                                    context.HttpContext.RequestAborted);


                            // -----------------------------------------
                            // User does not exist
                            // -----------------------------------------

                            if (user is null)
                            {
                                context.Fail(
                                    "User account was not found.");

                                return;
                            }


                            // -----------------------------------------
                            // User is inactive
                            // -----------------------------------------

                            if (!user.IsActive)
                            {
                                context.Fail(
                                    "User account is inactive.");

                                return;
                            }


                            // -----------------------------------------
                            // User is soft deleted
                            //
                            // GetByEmailAsync already excludes
                            // IsDeleted users, so null above handles it.
                            // -----------------------------------------
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(
                                "JWT USER STATUS CHECK FAILED");

                            Console.WriteLine(exception);

                            context.Fail(
                                "Unable to validate user account status.");
                        }
                    }
                };
            });


        // =========================================================
        // Authorization Policies
        // =========================================================

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "AdminOnly",
                policy =>
                    policy.RequireRole("Admin"));


            options.AddPolicy(
                "PatientManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            options.AddPolicy(
                "DepartmentView",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            options.AddPolicy(
                "DepartmentManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Receptionist"));


            options.AddPolicy(
                "DoctorView",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            options.AddPolicy(
                "DoctorManagement",
                policy =>
                    policy.RequireRole("Admin"));


            options.AddPolicy(
                "AppointmentManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            options.AddPolicy(
                "MedicalRecordManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor"));


            options.AddPolicy(
                "LaboratoryManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Lab Technician"));


            options.AddPolicy(
                "PharmacyManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Pharmacist"));


            options.AddPolicy(
                "PrescriptionManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Pharmacist"));


            options.AddPolicy(
                "InventoryManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Pharmacist"));


            options.AddPolicy(
                "BillingManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant",
                        "Receptionist"));


            options.AddPolicy(
                "PaymentManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant",
                        "Receptionist"));


            options.AddPolicy(
                "NotificationManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist",
                        "Pharmacist",
                        "Lab Technician",
                        "Accountant"));


            options.AddPolicy(
                "ReportsManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant"));


            options.AddPolicy(
                "DashboardAccess",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist",
                        "Pharmacist",
                        "Lab Technician",
                        "Accountant"));
        });


        return services;
    }
}