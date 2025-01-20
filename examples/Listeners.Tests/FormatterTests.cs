namespace MrKWatkins.Ast.Examples.Listeners.Tests;

public sealed class FormatterTests
{
    [Test]
    public void Format()
    {
        var expression = new Array(
            new Constant(1),
            new Constant(2),
            new Array(
                new Constant(10),
                new Constant(20)),
            new Constant(3));

        var actual = Formatter.Format(expression);

        actual.Should().Equal("[1, 2, [10, 20], 3]");
    }
}