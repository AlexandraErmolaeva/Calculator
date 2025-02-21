using Calculator.Application.Exceptions;
using Calculator.Application.Interfaces;

namespace Calculator.Application.Services;

public class MathOperationService : IMathOperationService
{
    private readonly IExpressionParser _expressionParser;

    public MathOperationService(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public double Sum(double[] numbers, CancellationToken token = default) => numbers.Sum();

    public double Subtract(double[] numbers, CancellationToken token = default)
    {
        var result = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            result -= numbers[i];
        }

        return result;
    }

    public double Multiply(double[] numbers, CancellationToken token = default)
    {
        var result = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            result *= numbers[i];

            if (result == 0)
                return 0;
        }

        return result;
    }

    public double Divide(double[] numbers, CancellationToken token = default)
    {
        var result = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] == 0)
                throw new DivideByZeroException("Cannot divide by zero value.");

            result /= numbers[i];

            if (result == 0)
                return 0;
        }

        return result;
    }

    public double RaiseToPower(double baseValue, double exponent, CancellationToken token = default)
    {
        return Math.Pow(baseValue, exponent);
    }

    public double ExtractRoot(double baseNumber, double rootDegree, CancellationToken token = default)
    {
        // Извлечение отрицательных корней работает только с нечетными степенями. 
        if (baseNumber < 0 && rootDegree % 2 == 0)
            throw new InvalidBaseValueException("Cannot extract the root of a negative number with an even degree.");

        if (rootDegree == 0)
            throw new InvalidRootDegreeException("Root degree cannot be zero.");

        if (baseNumber == 0)
            return 0;

        // Если основание отрицательное и степень нечётная, вычисляем корень из абсолютного значения и возвращаем с минусом.
        // MathPow не позволяет вычислять отрицательное основание с дробной степенью.
        if (baseNumber < 0)
        {
            double absResult = Math.Pow(Math.Abs(baseNumber), 1.0 / rootDegree);
            return -absResult;
        }

        return Math.Pow(baseNumber, 1.0 / rootDegree);
    }

    public double PowerFromBase(double baseNumber, double number, CancellationToken token = default)
    {
        // Основание логарифма не должно быть меньше 0 или равно 1.
        if (baseNumber <= 0 || baseNumber == 1)
            throw new InvalidBaseValueException("Base value of logarithm must be greater than zero and not equal 1.");

        // Значение логарифма не может быть отрицательным.
        if (number < 0)
            throw new LogarithmValueException("The number must be greater than zero for a logarithmic calculation.");

        // baseNumber ^ x = number;
        return Math.Log(number) / Math.Log(baseNumber);
    }

    public double CalculateMathExpression(string mathExpression, CancellationToken token = default)
    {
        return _expressionParser.ParseAndCalculate(mathExpression);
    }
}

