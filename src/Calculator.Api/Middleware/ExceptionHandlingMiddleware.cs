using Calculator.Application.Exceptions;
using Calculator.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace Calculator.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _nextDelegate;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _nextDelegate = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextDelegate(context);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Request was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while processing the request: {Path}.", context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    public static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,
            ArgumentException => HttpStatusCode.BadRequest,
            DivideByZeroException => HttpStatusCode.BadRequest,
            InvalidBaseValueException => HttpStatusCode.BadRequest,
            InvalidRootDegreeException => HttpStatusCode.BadRequest,
            LogarithmValueException => HttpStatusCode.BadRequest,
            MathExpressionException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var errorResponse = new
        {
            message = exception.Message,
            statusCode = (int)statusCode
        };

        var errorJson = JsonSerializer.Serialize(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(errorJson);
    }
}
