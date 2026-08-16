using System.Text;

using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Infrastructure.Authentication;
using MediCore.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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


                // =================================================
                // JWT EVENTS
                // =================================================

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        /*
                         * JWT validation proves that:
                         *
                         * 1. The token is correctly signed.
                         * 2. The issuer is valid.
                         * 3. The audience is valid.
                         * 4. The token has not expired.
                         *
                         * But JWT validation alone does NOT tell us
                         * whether the user is still active in the
                         * database.
                         *
                         * Therefore, after the JWT has been validated,
                         * we check the current User record.
                         */

                        var userIdClaim =
                            context.Principal?
                                .FindFirst(
                                    System.Security.Claims.ClaimTypes.NameIdentifier)?
                                .Value;


                        /*
                         * If the token does not contain a valid
                         * user identifier, reject the authentication.
                         */

                        if (!int.TryParse(
                                userIdClaim,
                                out var userId))
                        {
                            context.Fail(
                                "User identity could not be determined.");

                            return;
                        }


                        /*
                         * Resolve IUserRepository from the current
                         * request service provider.
                         */

                        var userRepository =
                            context.HttpContext.RequestServices
                                .GetRequiredService<IUserRepository>();


                        /*
                         * Load the current user from the database.
                         *
                         * GetByIdAsync already excludes deleted users.
                         */

                        var user =
                            await userRepository.GetByIdAsync(
                                userId,
                                context.HttpContext.RequestAborted);


                        /*
                         * User does not exist or has been deleted.
                         */

                        if (user is null)
                        {
                            context.Fail(
                                "User account was not found.");

                            return;
                        }


                        /*
                         * IMPORTANT:
                         *
                         * The user may have been active when the JWT
                         * was issued but deactivated afterwards.
                         *
                         * Always use the current database value.
                         */

                        if (!user.IsActive)
                        {
                            context.Fail(
                                "User account is inactive.");

                            return;
                        }
                    },


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
                    }
                };
            });


        // =========================================================
        // Authorization Policies
        // =========================================================

        services.AddAuthorization(options =>
        {
            // -----------------------------------------------------
            // Administration
            // -----------------------------------------------------

            options.AddPolicy(
                "AdminOnly",
                policy =>
                    policy.RequireRole("Admin"));


            // -----------------------------------------------------
            // Patients
            // -----------------------------------------------------

            options.AddPolicy(
                "PatientManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            // -----------------------------------------------------
            // Departments
            // -----------------------------------------------------

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


            // -----------------------------------------------------
            // Doctors
            // -----------------------------------------------------

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
                    policy.RequireRole(
                        "Admin"));


            // -----------------------------------------------------
            // Appointments
            // -----------------------------------------------------

            options.AddPolicy(
                "AppointmentManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Receptionist"));


            // -----------------------------------------------------
            // Medical Records
            // -----------------------------------------------------

            options.AddPolicy(
                "MedicalRecordManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor"));


            // -----------------------------------------------------
            // Laboratory
            // -----------------------------------------------------

            options.AddPolicy(
                "LaboratoryManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Lab Technician"));


            // -----------------------------------------------------
            // Pharmacy
            // -----------------------------------------------------

            options.AddPolicy(
                "PharmacyManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Pharmacist"));


            // -----------------------------------------------------
            // Prescriptions
            // -----------------------------------------------------

            options.AddPolicy(
                "PrescriptionManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Doctor",
                        "Pharmacist"));


            // -----------------------------------------------------
            // Inventory
            // -----------------------------------------------------

            options.AddPolicy(
                "InventoryManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Pharmacist"));


            // -----------------------------------------------------
            // Billing
            // -----------------------------------------------------

            options.AddPolicy(
                "BillingManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant",
                        "Receptionist"));


            // -----------------------------------------------------
            // Payments
            // -----------------------------------------------------

            options.AddPolicy(
                "PaymentManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant",
                        "Receptionist"));


            // -----------------------------------------------------
            // Notifications
            // -----------------------------------------------------

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


            // -----------------------------------------------------
            // Reports
            // -----------------------------------------------------

            options.AddPolicy(
                "ReportsManagement",
                policy =>
                    policy.RequireRole(
                        "Admin",
                        "Accountant"));


            // -----------------------------------------------------
            // Dashboard
            // -----------------------------------------------------

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