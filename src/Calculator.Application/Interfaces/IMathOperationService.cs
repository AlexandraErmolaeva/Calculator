namespace Calculator.Application.Interfaces;

public interface IMathOperationService
{
    double CalculateMathExpression(string mathExpression, CancellationToken token = default);
    double Divide(double[] numbers, CancellationToken token = default);
    double ExtractRoot(double baseNumber, double rootDegree, CancellationToken token = default);
    double Multiply(double[] numbers, CancellationToken token = default);
    double PowerFromBase(double baseNumber, double number, CancellationToken token = default);
    double RaiseToPower(double baseValue, double exponent, CancellationToken token = default);
    double Subtract(double[] numbers, CancellationToken token = default);
    double Sum(double[] numbers, CancellationToken token = default);
}