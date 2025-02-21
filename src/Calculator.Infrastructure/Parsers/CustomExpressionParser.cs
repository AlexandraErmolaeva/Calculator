using Calculator.Application.Interfaces;
using Calculator.Infrastructure.Exceptions;
using System.Globalization;

namespace Calculator.Infrastructure.Parsers;

// TODO: Доработать. Проблема - повторение скобок, например 1 * (345 - ((4 / 6) ^ 6));
public class CustomExpressionParser : IExpressionParser
{
    public double ParseAndCalculate(string mathExpression)
    {
        try
        {
            // Заводим словарь с приоритетом арифметических операций.
            var operators = new Dictionary<char, int>
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 }
        };

            var numbers = new Stack<double>();
            var expressionOperators = new Stack<char>();

            // Разделяем строку на операторы и операнды.
            var tokens = SplitIntoTokens(mathExpression);

            foreach (var token in tokens)
            {
                // Получаем операнд.
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                    numbers.Push(number);

                else if (token == "(")
                    expressionOperators.Push('(');

                else if (token == ")")
                {
                    // Пока не найдется открывающая скобка, вычисляем выражение, находящееся до этой открывающей скобки.
                    while (expressionOperators.Peek() != '(')
                    {
                        ProcessOperator(expressionOperators.Pop(), numbers);
                    }

                    // Убираем из стека открывающую скобку.
                    expressionOperators.Pop();
                }

                else if (operators.ContainsKey(token[0]))
                {
                    while (expressionOperators.Count > 0 && operators.ContainsKey(expressionOperators.Peek()) && operators[expressionOperators.Peek()] >= operators[token[0]])
                    {
                        // Если стек не пустой и оператор находится в словаре, при этом приоритет текущего оператора выше предыдущего, рассчитываем.
                        ProcessOperator(expressionOperators.Pop(), numbers);
                    }

                    // Добавляем оператор в стек.
                    expressionOperators.Push(token[0]);
                }
            }

            while (expressionOperators.Count > 0)
            {
                // Досчитываем остатки.
                ProcessOperator(expressionOperators.Pop(), numbers);
            }

            return numbers.Pop();
        }
        catch (Exception)
        {
            throw new MathExpressionException("There was an error processing the math expression.");
        }
    }

    private List<string> SplitIntoTokens(string mathExpression)
    {
        var tokens = new List<string>();
        var operators = new HashSet<char>("+-*/^()");

        var curerntValue = string.Empty;

        foreach (var value in mathExpression)
        {
            if (char.IsWhiteSpace(value))
                continue;

            if (char.IsDigit(value) || value == '.')
                curerntValue += value;

            if (operators.Contains(value))
            {
                if (!string.IsNullOrEmpty(curerntValue))
                {
                    tokens.Add(curerntValue);

                    curerntValue = string.Empty;
                }

                tokens.Add(value.ToString());
            }
        }

        if (!string.IsNullOrEmpty(curerntValue))
            tokens.Add(curerntValue);

        return tokens;
    }

    private void ProcessOperator(char operation, Stack<double> numbers)
    {
        double secondNumber = numbers.Pop();
        double firstNumber = numbers.Pop();

        switch (operation)
        {
            case '+':
                numbers.Push(firstNumber + secondNumber);
                break;

            case '-':
                numbers.Push(firstNumber - secondNumber);
                break;

            case '*':
                numbers.Push(firstNumber * secondNumber);
                break;

            case '/':
                numbers.Push(firstNumber / secondNumber);
                break;

            case '^':
                numbers.Push(Math.Pow(firstNumber, secondNumber));
                break;
        }
    }
}
