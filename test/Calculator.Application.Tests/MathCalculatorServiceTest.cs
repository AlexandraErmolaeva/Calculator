using Calculator.Application.Exceptions;
using Calculator.Application.Interfaces;
using Calculator.Application.Services;
using Calculator.Infrastructure.Exceptions;
using Moq;

namespace Calculator.Application.Test;

public class MathCalculatorServiceTest
{
    private readonly Mock<IExpressionParser> _expressionParserMock;
    private readonly IMathOperationService _mathCalculatorService;

    public MathCalculatorServiceTest()
    {
        _expressionParserMock = new Mock<IExpressionParser>();
        _mathCalculatorService = new MathOperationService(_expressionParserMock.Object);
    }

    [Fact]
    public void Sum_ValidNumbers_ReturnsSum()
    {
        double[] numbers = { 1, 586, 3, -8596, 45, 420 };

        var expectedResult = -7541.0;

        var result = _mathCalculatorService.Sum(numbers);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Subtract_ValidNumbers_ReturnsDifference()
    {
        double[] numbers = { 1, 586, 3, -8596, 45, 420 };

        var expectedResult = 7543.0;

        var result = _mathCalculatorService.Subtract(numbers);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Multiply_ValidNumbers_ReturnsProduct()
    {
        double[] numbers = { 1, 586, 3, -8596, 45, 420 };

        var expectedResult = -285612415200.0;

        var result = _mathCalculatorService.Multiply(numbers);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Multiply_ValidNumbers_ReturnsZero()
    {
        double[] numbers = { 1, 586, 3, -8596, 45, 420, 0 };

        var expectedResult = 0.0;

        var result = _mathCalculatorService.Multiply(numbers);

        Assert.Equal(expectedResult, result);
    }

    // Для нуля проверить деление на 0.
    [Fact]
    public void Divide_ValidNumbers_ReturnsQuotient()
    {
        double[] numbers = { 1, 586, 3, 45, 420 };

        var expectedResult = 3.0096730893090392521564307684899e-8;

        var result = _mathCalculatorService.Divide(numbers);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Divide_DivisionByZero_ThrowsDivideByZeroException()
    {
        double[] numbers = { 10, 0, 2 };

        // Пока не выбросили ошибку вручную, тест не работал. (?)
        Assert.Throws<DivideByZeroException>(() => _mathCalculatorService.Divide(numbers));
    }

    [Fact]
    public void RaiseToPower_ValidInput_ReturnsPower()
    {
        var baseValue = 28.0;
        var exponent = 7.0;

        var expectedResult = 13492928512.0;

        var result = _mathCalculatorService.RaiseToPower(baseValue, exponent);

        Assert.Equal(expectedResult, result);
    }

    // Проверить извлечение корней из отрицательного числа.
    [Fact]
    public void ExtractRoot_ValidInput_ReturnsRoot()
    {
        var baseNumber = -16.0;
        var rootDegree = 3.0;

        var expectedResult = -2.5198420997897464;

        var result = _mathCalculatorService.ExtractRoot(baseNumber, rootDegree);

        Assert.Equal(expectedResult, result);
    }

    // Основание корня не должно быть < 0 при четной степени.
    [Fact]
    public void ExtractRoot_EvenDegreeNegativeNumber_ThrowsInvalidBaseValueException()
    {
        var baseNumber = -16.0;
        var rootDegree = 2.0;

        Assert.Throws<InvalidBaseValueException>(() => _mathCalculatorService.ExtractRoot(baseNumber, rootDegree));
    }

    // Степень корня != 0.
    [Fact]
    public void ExtractRoot_ZeroValueRootDegreeNumber_ThrowsInvalidBaseValueException()
    {
        var baseNumber = 360.0;
        var rootDegree = 0.0;

        Assert.Throws<InvalidRootDegreeException>(() => _mathCalculatorService.ExtractRoot(baseNumber, rootDegree));
    }

    [Fact]
    public void PowerFromBase_ValidInput_ReturnsPow()
    {
        var baseNumber = 9.0;
        var number = 81.0;

        var expectedResult = 2.0;

        var result = _mathCalculatorService.PowerFromBase(baseNumber, number);

        Assert.Equal(expectedResult, result);
    }

    // Основание логарифма != 1.
    [Fact]
    public void PowerFromBase_InvalidBase_ThrowsInvalidBaseValueException()
    {
        var baseNumber = 1.0;
        var number = 10.0;

        Assert.Throws<InvalidBaseValueException>(() => _mathCalculatorService.PowerFromBase(baseNumber, number));
    }

    // Само число не может быть < 0.
    [Fact]
    public void PowerFromBase_InvalidNumber_ThrowsLogarithmValueException()
    {
        var baseNumber = 10.0;
        var number = -5.0;

        Assert.Throws<LogarithmValueException>(() => _mathCalculatorService.PowerFromBase(baseNumber, number));
    }

    [Fact]
    public void CalculateMathExpression_ValidExpression_CallsParserAndReturnsResult() // TODO: Работаем очень медленно! Нужен асинхрон.
    {
        var expression = "2* 6 + 4(-99 * Sqrt(16)) - 560";

        var expectedResult = -2132.0;

        _expressionParserMock.Setup(p => p.ParseAndCalculate(expression)).Returns(expectedResult);

        var result = _mathCalculatorService.CalculateMathExpression(expression);

        Assert.Equal(expectedResult, result);

        // Проверяем, вызвалися ли парсер только единожды.
        _expressionParserMock.Verify(p => p.ParseAndCalculate(expression), Times.Once());
    }

    [Fact]
    public void CalculateMathExpression_ParserThrowsException_PropagatesException()
    {
        string expression = "2 + + 3())(";

        _expressionParserMock.Setup(p => p.ParseAndCalculate(expression))
            .Throws(new MathExpressionException("Invalid expression syntax."));

        Assert.Throws<MathExpressionException>(() => _mathCalculatorService.CalculateMathExpression(expression));

        _expressionParserMock.Verify(p => p.ParseAndCalculate(expression), Times.Once());
    }
}