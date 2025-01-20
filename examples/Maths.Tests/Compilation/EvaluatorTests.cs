using MrKWatkins.Ast.Examples.Maths.Compilation;
using MrKWatkins.Ast.Examples.Maths.Parsing;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Compilation;

public sealed class CompilerTests
{
    [Test]
    public void Compile_ExpressionHasErrors()
    {
        var function = Parser.Parse("2 / 0");

        AssertThat.Invoking(() => Compiler.Compile(function)).Should().Throw<ArgumentException>();
    }

    [TestCase("1", 1)]
    [TestCase("1 + 2", 3)]
    [TestCase("1 + 2 * 3", 7)]
    public void Compile_NoParameters(string expression, int expected)
    {
        var function = Parser.Parse(expression);

        var compiled = Compiler.Compile(function);

        var func = compiled.Should().BeOfType<Func<int>>().Value;

        func().Should().Equal(expected);
    }

    [TestCase("2 + a", 7, 5)]
    [TestCase("(a - 2) / 5", 2, 12)]
    public void Compile_OneParameter(string expression, int expected, int argument)
    {
        var function = Parser.Parse(expression);

        var compiled = Compiler.Compile(function);

        var func = compiled.Should().BeOfType<Func<int, int>>().Value;

        func(argument).Should().Equal(expected);
    }

    [TestCase("a * b", 35, 5, 7)]
    [TestCase("(a - 2) / b", 5, 12, 2)]
    public void Compile_TwoParameters(string expression, int expected, int argument0, int argument1)
    {
        var function = Parser.Parse(expression);

        var compiled = Compiler.Compile(function);

        var func = compiled.Should().BeOfType<Func<int, int, int>>().Value;

        func(argument0, argument1).Should().Equal(expected);
    }
}