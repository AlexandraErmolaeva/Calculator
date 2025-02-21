using Calculator.Application.Interfaces;
using Calculator.Infrastructure.Exceptions;

namespace Calculator.Infrastructure.Test;

public abstract class ExpressionParserBaseTest
{
    protected readonly IExpressionParser _parser;

    protected ExpressionParserBaseTest(IExpressionParser parser)
    {
        _parser = parser;
    }

    [Fact]
    public void ParseAndCalculate_ReturnsCorrectResult()
    {
        var expression = "2 + 3*50^(1/2) - 140 * 12-(-4 * 1.5698)";
        var expectedResult = -1650.5075965644035735;

        var result = _parser.ParseAndCalculate(expression);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ParseAndEvaluate_NullOrEmptyExpression_ThrowsMathExpressionException()
    {
        var expression = "";

        Assert.Throws<MathExpressionException>(() => _parser.ParseAndCalculate(expression));
    }

    [Fact]
    public void ParseAndEvaluate_InvalidSyntax_ThrowsMathExpressionException()
    {
        var expression = "2 + + 3(()";

        Assert.Throws<MathExpressionException>(() => _parser.ParseAndCalculate(expression));
    }
}
