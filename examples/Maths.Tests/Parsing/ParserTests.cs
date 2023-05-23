using MrKWatkins.Ast.Examples.Maths.Parsing;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Parsing;

public sealed class ParserTests : TestFixture
{
    [TestCase("1", "() => 1")]
    [TestCase("1 + 2 * 3", "() => (+ 1 (* 2 3))")]
    [TestCase("a + b * c * d + e", "(a, b, c, d, e) => (+ (+ a (* (* b c) d)) e)")]
    [TestCase("(((0)))", "() => 0")]
    [TestCase("(1 + x) * 3", "(x) => (* (+ 1 x) 3)")]
    public void Parse(string expression, string expected)
    {
        var function = ParseWithoutProcessing(expression);

        function.ToString().Should().BeEquivalentTo(expected);
    }
    
    [TestCase("(1", "EndOfFile EOF", 2)]
    [TestCase(")", "CloseBracket )", 0)]
    [TestCase("1 2", "Number 2", 2)]
    public void Parse_UnexpectedToken(string expression, string token, int index) =>
        FluentActions.Invoking(() => Parser.Parse(expression))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"Unexpected token {token} at index {index}.");
}