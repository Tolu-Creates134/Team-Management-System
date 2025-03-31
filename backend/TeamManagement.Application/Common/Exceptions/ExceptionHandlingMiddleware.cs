using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TeamManagementSystem.Application.Common.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Proceed with request pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new { message = "An unexpected error occurred." }; // Default response
        var statusCode = HttpStatusCode.InternalServerError; // Default 500

        switch (exception)
        {
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized; // 401
                errorResponse = new { message = exception.Message };
                break;

            case ValidationException:
                statusCode = HttpStatusCode.BadRequest; // 400
                errorResponse = new { message = exception.Message };
                break;

            case UserAlreadyExistsException:
                statusCode = HttpStatusCode.Conflict; // 409
                errorResponse = new { message = exception.Message};
                break;
            default:
                _logger.LogError(exception, "Unhandled Exception: {Message}", exception.Message);
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
