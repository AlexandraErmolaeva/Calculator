using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Calculator.Api.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    private const string FilterName = nameof(LoggingFilter);

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerName = context.ActionDescriptor.RouteValues["controller"];

        var actionName = context.ActionDescriptor.RouteValues["action"];

        _logger.LogInformation("[{FilterName}]: Calling controller '{ControllerName}' with action '{ActionName}'.", FilterName, controllerName, actionName);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var controllerName = context.ActionDescriptor.RouteValues["controller"];

        var actionName = context.ActionDescriptor.RouteValues["action"];

        if (context.Exception != null)
            _logger.LogError("[{FilterName}]: Action '{ControllerName}.{ActionName}' failed with error.", FilterName, controllerName, actionName);

        else if (context.Result is IStatusCodeActionResult statusResult)
        {
            var statusCode = statusResult.StatusCode ?? 200;

            if (statusCode >= 400)
            {
                var errorMessage = context.Result is ObjectResult objectResult && objectResult.Value != null
                                   ? objectResult.Value.ToString() : "No additional error details.";

                _logger.LogWarning("[{FilterName}]: Action '{ControllerName}.{ActionName}' completed with error status {StatusCode}.", FilterName, controllerName, actionName, statusCode);
            }

            //else if (context.Result is ObjectResult objectResult)
            //    _logger.LogInformation("[{FilterName}]: Action '{ControllerName}.{ActionName}' completed with result.", FilterName, controllerName, actionName);

            else
                _logger.LogInformation("[{FilterName}]: Action '{ControllerName}.{ActionName}' completed without result", FilterName, controllerName, actionName);
        }
    }
}
