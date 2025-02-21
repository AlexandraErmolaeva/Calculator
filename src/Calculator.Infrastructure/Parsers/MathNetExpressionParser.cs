using Calculator.Application.Interfaces;
using Calculator.Infrastructure.Exceptions;
using MathNet.Symbolics;

namespace Calculator.Infrastructure.Parsers;

public class MathNetExpressionParser : IExpressionParser
{
    public double ParseAndCalculate(string mathExpression)
    {
        try
        {
            var expression = SymbolicExpression.Parse(mathExpression.ToLowerInvariant());

            var variables = new Dictionary<string, FloatingPoint>();

            var result = expression.Evaluate(variables).RealValue;

            return result;
        }
        catch (Exception ex)
        {
            throw new MathExpressionException($"Invalid expression or evaluation error: {ex.Message}");
        }
    }
}

