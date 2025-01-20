using MrKWatkins.Ast.Examples.Maths.Tree;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Tree;

public sealed class ExpressionTests
{
    [Test]
    public void ToString_Test()
    {
        var root = new BinaryOperation('+', new Constant(1), new BinaryOperation('*', new Variable("two"), new Constant(3)));

        root.ToString().Should().SequenceEqual("(+ 1 (* two 3))");
    }
}