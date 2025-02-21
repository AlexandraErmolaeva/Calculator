using Calculator.Application.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace Calculator.Api.Filters;

public class ExpressionConversionFilter : IActionFilter
{
    private static readonly Regex WhitespaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

    // Убираем все пробелы из входящей строки и приводим к нижнему регистру.
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("dto", out var dto) && dto is MathExpressionRequestDto expressionDto)
        {
            var expression = expressionDto.Expression.ToLower();

            expression = WhitespaceRegex.Replace(expressionDto.Expression, "");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
