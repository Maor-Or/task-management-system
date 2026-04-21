using System.Net;
using System.Text.Json;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
       _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

        context.Response.ContentType = "application/json";

        int statusCode = 500;
        string message = "An unexpected error occurred";

        if(ex is AppException appEx)
        {
            statusCode = appEx.StatusCode;
            message = appEx.Message;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            message,
            statusCode
        };
        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}