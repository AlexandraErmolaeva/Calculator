namespace Calculator.Application.Interfaces;

public interface IExpressionParser
{
    double ParseAndCalculate(string mathExpression);
}