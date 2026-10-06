namespace MrKWatkins.Ast.Examples.Visitors.Tests;

public sealed class EvaluatorTests
{
    [TestCase(false, false, false)]
    [TestCase(false, true, false)]
    [TestCase(true, false, false)]
    [TestCase(true, true, true)]
    public void Evaluate_And(bool left, bool right, bool expected)
    {
        var expression = new And(new Constant(left), new Constant(right));

        Evaluator.Evaluate(expression).Should().Equal(expected);
    }

    [TestCase(false, false, false)]
    [TestCase(false, true, true)]
    [TestCase(true, false, true)]
    [TestCase(true, true, true)]
    public void Evaluate_Or(bool left, bool right, bool expected)
    {
        var expression = new Or(new Constant(left), new Constant(right));

        Evaluator.Evaluate(expression).Should().Equal(expected);
    }

    [TestCase(false, true)]
    [TestCase(true, false)]
    public void Evaluate_Not(bool operand, bool expected)
    {
        var expression = new Not(new Constant(operand));

        Evaluator.Evaluate(expression).Should().Equal(expected);
    }

    [Test]
    public void Evaluate_Variables()
    {
        // (a || b) && !c
        var expression = new And(
            new Or(new Variable("a"), new Variable("b")),
            new Not(new Variable("c")));

        var variables = new Dictionary<string, bool> { ["a"] = false, ["b"] = true, ["c"] = false };

        Evaluator.Evaluate(expression, variables).Should().BeTrue();

        variables["c"] = true;

        Evaluator.Evaluate(expression, variables).Should().BeFalse();
    }

    [Test]
    public void Evaluate_VariableWithNoValue()
    {
        var expression = new Variable("a");

        AssertThat.Invoking(() => Evaluator.Evaluate(expression)).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Evaluate_And_ShortCircuits()
    {
        // The right operand is a variable with no value, so evaluating it would throw. It is never visited because the left operand is false.
        var expression = new And(new Constant(false), new Variable("a"));

        Evaluator.Evaluate(expression).Should().BeFalse();
    }

    [Test]
    public void Evaluate_Or_ShortCircuits()
    {
        var expression = new Or(new Constant(true), new Variable("a"));

        Evaluator.Evaluate(expression).Should().BeTrue();
    }
}