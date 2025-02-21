using Calculator.Application.Interfaces;
using Calculator.Application.Services;
using Calculator.Infrastructure.Parsers;

namespace Calculator.Api.Extensions;

public static class DependencyEnjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculationApplicationService, CalculationApplicationService>();
        services.AddScoped<IMathOperationService, MathOperationService>();
        services.AddScoped<IExpressionParser, MathNetExpressionParser>();

        return services;
    }
}
