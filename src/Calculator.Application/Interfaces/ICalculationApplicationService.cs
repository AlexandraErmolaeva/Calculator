using Calculator.Application.Dtos;

namespace Calculator.Application.Interfaces
{
    public interface ICalculationApplicationService
    {
        CalculatedResponseDto Add(MathOperandsRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto CalculateMathExpression(MathExpressionRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto Divide(MathOperandsRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto ExtractRoot(RootRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto Multiply(MathOperandsRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto PowerFromBase(PowerFromBaseRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto RaiseToPower(PowerRequestDto dto, CancellationToken token = default);
        CalculatedResponseDto Subtract(MathOperandsRequestDto dto, CancellationToken token = default);
    }
}