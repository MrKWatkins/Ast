using MrKWatkins.Ast.Examples.Maths.Evaluation;
using MrKWatkins.Ast.Examples.Maths.Parsing;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Evaluation;

public sealed class EvaluatorTests
{
    [Test]
    public void Evaluate_ExpressionHasErrors()
    {
        var function = Parser.Parse("2 / 0");

        FluentActions.Invoking(() => Evaluator.Evaluate(function)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Evaluate_WrongNumberOfArguments()
    {
        var function = Parser.Parse("a + b");

        FluentActions.Invoking(() => Evaluator.Evaluate(function, 5)).Should().Throw<ArgumentException>();
    }

    [TestCase("1", 1)]
    [TestCase("1 + 2", 3)]
    [TestCase("1 + 2 * 3", 7)]
    [TestCase("2 + a", 7, 5)]
    [TestCase("a + b * c", 47, 5, 6, 7)]
    [TestCase("(a - 2) / 5", 2, 12)]
    public void Evaluate(string expression, int expected, params int[] arguments)
    {
        var function = Parser.Parse(expression);

        var actual = Evaluator.Evaluate(function, arguments);

        actual.Should().Be(expected);
    }
}