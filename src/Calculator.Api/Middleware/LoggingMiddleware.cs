namespace Calculator.Api.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var requestPath = context.Request.Path;
        var queryString = context.Request.QueryString;

        _logger.LogInformation("[{Method}]: Incoming Request: {Path}.", method, requestPath);

        await _next(context);

        var statusCode = context.Response.StatusCode;

        _logger.LogInformation("[{Method}]: Response: {StatusCode}.", method, statusCode);
    }
}
