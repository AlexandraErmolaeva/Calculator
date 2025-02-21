using Calculator.Infrastructure.Parsers;
using Calculator.Infrastructure.Test;
using Xunit.Abstractions;

namespace Calculator.Tests.Parsers
{
    public class MathNetExpressionParserTest : ExpressionParserBaseTest
    {
        private readonly ITestOutputHelper _output;
        public MathNetExpressionParserTest(ITestOutputHelper output) : base(new MathNetExpressionParser())
        {
            _output = output;
        }

        [Fact]
        public void ParseAndCalculate_SqrtOperation_ReturnsCorrectResult()
        {
            var expression = "SqRt(64)";
            var expectedResult = 8.0;

            var result = _parser.ParseAndCalculate(expression);

            Assert.Equal(expectedResult, result, precision: 10);
        }

        [Fact]
        public void ParseAndCalculate_ArrayOperation_ReturnsCorrectResult()
        {
            var parts = new[]
            {
                "5*96/52*9-45",
                "64/sqrt(64)",
                "(42*9-9)",
                "((424*24)^2)"
            };

            var expectedResult = new[]
            {
                38.07692307692308,
                8,
                369,
                103550976
            };

            for (int i = 0; i < parts.Length; i++)
            {
                var result = _parser.ParseAndCalculate(parts[i]);

                _output.WriteLine($"Part {parts[i]} = {result}");

                Assert.Equal(expectedResult[i], result, precision: 10);
            }
        }
    }
}