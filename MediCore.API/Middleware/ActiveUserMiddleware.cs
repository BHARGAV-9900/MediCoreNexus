using System.Security.Claims;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.API.Middleware;

public class ActiveUserMiddleware
{
    private readonly RequestDelegate _next;

    public ActiveUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserRepository userRepository)
    {
        // ---------------------------------------------------------
        // Only check authenticated users.
        //
        // Anonymous endpoints such as Login should continue
        // normally.
        // ---------------------------------------------------------

        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var userIdClaim =
                context.User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

            // -----------------------------------------------------
            // If the authenticated request does not contain a
            // valid user ID, reject it.
            // -----------------------------------------------------

            if (!int.TryParse(userIdClaim, out var userId))
            {
                context.Response.StatusCode =
                    StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = "Invalid user identity."
                    });

                return;
            }

            // -----------------------------------------------------
            // Read the CURRENT user state from the database.
            //
            // This is important:
            // We are NOT trusting the JWT for IsActive.
            // -----------------------------------------------------

            var user =
                await userRepository.GetByIdAsync(
                    userId,
                    context.RequestAborted);

            // -----------------------------------------------------
            // User doesn't exist / deleted
            // -----------------------------------------------------

            if (user is null || user.IsDeleted)
            {
                context.Response.StatusCode =
                    StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message =
                            "User account is no longer available."
                    });

                return;
            }

            // -----------------------------------------------------
            // USER DEACTIVATED
            // -----------------------------------------------------

            if (!user.IsActive)
            {
                context.Response.StatusCode =
                    StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message =
                            "User account is inactive."
                    });

                return;
            }
        }

        // ---------------------------------------------------------
        // User is active → continue normally.
        // ---------------------------------------------------------

        await _next(context);
    }
}