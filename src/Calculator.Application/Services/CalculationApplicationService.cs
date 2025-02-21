using Calculator.Application.Dtos;
using Calculator.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Calculator.Application.Services;

public class CalculationApplicationService : ICalculationApplicationService
{
    private readonly IMathOperationService _mathCalculatorService;
    private readonly ILogger<CalculationApplicationService> _logger;

    private const string ServiceName = nameof(CalculationApplicationService);

    public CalculationApplicationService(IMathOperationService mathCalculatorService, ILogger<CalculationApplicationService> logger)
    {
        _mathCalculatorService = mathCalculatorService;
        _logger = logger;
    }

    public CalculatedResponseDto Add(MathOperandsRequestDto dto, CancellationToken token = default)
    {
        var operationName = nameof(Add).ToLower();

        return CalculateArrayOperation(dto, numbers => _mathCalculatorService.Sum(numbers), operationName, token);
    }

    public CalculatedResponseDto Subtract(MathOperandsRequestDto dto, CancellationToken token = default)
    {
        var operationName = nameof(Subtract).ToLower();

        return CalculateArrayOperation(dto, numbers => _mathCalculatorService.Subtract(numbers), operationName, token);
    }

    public CalculatedResponseDto Multiply(MathOperandsRequestDto dto, CancellationToken token = default)
    {
        var operationName = nameof(Multiply).ToLower();

        return CalculateArrayOperation(dto, numbers => _mathCalculatorService.Multiply(numbers), operationName, token);
    }

    public CalculatedResponseDto Divide(MathOperandsRequestDto dto, CancellationToken token = default)
    {
        var operationName = nameof(Divide).ToLower();

        return CalculateArrayOperation(dto, numbers => _mathCalculatorService.Divide(numbers), operationName, token);
    }

    public CalculatedResponseDto RaiseToPower(PowerRequestDto dto, CancellationToken token = default)
    {
        var baseValue = dto.BaseValue;
        var exponent = dto.Exponent;

        _logger.LogInformation("[{ServiceName}]: Starting to raise {Base} to power {Exponent}...", ServiceName, baseValue, exponent);

        var result = _mathCalculatorService.RaiseToPower(baseValue, exponent);

        return CreateResponse(result);
    }

    public CalculatedResponseDto ExtractRoot(RootRequestDto dto, CancellationToken token = default)
    {
        var baseNumber = dto.BaseNumber;
        var rootDegree = dto.RootDegree;

        _logger.LogInformation("[{ServiceName}]: Starting to extract root of degree {RootDegree} from {BaseNumber}.", ServiceName, rootDegree, baseNumber);

        var result = _mathCalculatorService.ExtractRoot(baseNumber, rootDegree);

        return CreateResponse(result);
    }

    public CalculatedResponseDto PowerFromBase(PowerFromBaseRequestDto dto, CancellationToken token = default)
    {
        var baseNumber = dto.BaseNumber;
        var number = dto.Number;

        _logger.LogInformation("[{ServiceName}]: Starting to find exponent x where {BaseNumber}^x = {Number}.", ServiceName, baseNumber, number);

        var result = _mathCalculatorService.PowerFromBase(baseNumber, number);

        return CreateResponse(result);
    }

    public CalculatedResponseDto CalculateMathExpression(MathExpressionRequestDto dto, CancellationToken token = default)
    {
        var expression = dto.Expression;

        _logger.LogInformation("[{ServiceName}]: Starting to calculate math expression: {Expression}...", ServiceName, expression);

        var result = _mathCalculatorService.CalculateMathExpression(expression);

        return CreateResponse(result);
    }

    private CalculatedResponseDto CalculateArrayOperation(MathOperandsRequestDto dto, Func<double[], double> operation, string operationName, CancellationToken token = default)
    {
        _logger.LogInformation("[{ServiceName}]: Starting to {Operation} numbers...", ServiceName, operationName);

        var numbers = dto.Numbers;

        var result = operation(numbers);

        return CreateResponse(result);
    }

    private CalculatedResponseDto CreateResponse(double result)
    {
        return new CalculatedResponseDto { Result = result };
    }
}
