using FluentValidation;
using MediCore.Application.Exceptions;
using MediCore.Shared.Responses;
using System.Text.Json;

namespace MediCore.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.FailureResponse(
                "Validation failed.",
                ex.Errors.Select(e => e.ErrorMessage));

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.FailureResponse(
                ex.Message);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.FailureResponse(
                ex.Message);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (UnauthorizedException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.FailureResponse(
                ex.Message);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception. RequestId: {RequestId}, Method: {Method}, Path: {Path}",
                context.TraceIdentifier,
                context.Request.Method,
                context.Request.Path);

            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.FailureResponse(
                "An unexpected error occurred.");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
