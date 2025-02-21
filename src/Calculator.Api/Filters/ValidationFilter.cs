using Calculator.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace Calculator.Api.Filters;

public class ValidationFilter : IActionFilter
{
    private static readonly Regex ValidMathExpression = new Regex(@"^[0-9\s\.\+\-\*/\^\(\)]*(sqrt\([0-9\s\.\+\-\*/\^\(\)]*\))*[0-9\s\.\+\-\*/\^\(\)]*$", RegexOptions.Compiled);

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);

            return;
        }

        if (context.ActionArguments.TryGetValue("dto", out var dto))
        {
            switch (dto)
            {
                case MathOperandsRequestDto operandsDto:

                    if (!IsNumbersInValidRange(operandsDto.Numbers))
                    {
                        context.Result = new BadRequestObjectResult("Input numbers contain invalid values.");

                        return;
                    }
                    break;

                case MathExpressionRequestDto expressionDto:

                    if (!ValidMathExpression.IsMatch(expressionDto.Expression))
                    {
                        context.Result = new BadRequestObjectResult("Expression contains invalid characters.");

                        return;
                    }
                    break;

                case PowerFromBaseRequestDto powFromBaseDto:

                    if (!IsNumbersInValidRange(ExtractNumbers(powFromBaseDto)))
                    {
                        context.Result = new BadRequestObjectResult("Input numbers contain invalid values.");

                        return;
                    }
                    break;

                case PowerRequestDto powDto:

                    if (!IsNumbersInValidRange(ExtractNumbers(powDto)))
                    {
                        context.Result = new BadRequestObjectResult("Input numbers contain invalid values.");

                        return;
                    }
                    break;

                case RootRequestDto rootDto:

                    if (!IsNumbersInValidRange(ExtractNumbers(rootDto)))
                    {
                        context.Result = new BadRequestObjectResult("Input numbers contain invalid values.");

                        return;
                    }
                    break;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Здесь выполняем действия уже после выполнения в контроллере.
    }

    private static double[] ExtractNumbers<T>(T dto)
    {
        var numbers = new List<double>();

        switch (dto)
        {
            case PowerFromBaseRequestDto pow:
                numbers.Add(pow.BaseNumber);
                numbers.Add(pow.Number);
                break;

            case PowerRequestDto power:
                numbers.Add(power.BaseValue);
                numbers.Add(power.Exponent);
                break;

            case RootRequestDto root:
                numbers.Add(root.BaseNumber);
                numbers.Add(root.RootDegree);
                break;
        }

        return numbers.ToArray();
    }

    private bool IsNumbersInValidRange(double[] numbers)
    {
        if (numbers.Any(number => number >= double.MaxValue ||
                                  number <= double.MinValue ||
                                  double.IsNaN(number) ||
                                  double.IsInfinity(number)))
        {
            return false;
        }

        return true;
    }
}