using MrKWatkins.Ast.Examples.Maths.Processing;
using MrKWatkins.Ast.Examples.Maths.Tree;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Processing;

public sealed class DivideByZeroValidatorTests : TestFixture
{
    [TestCase("2 * 3")]
    [TestCase("2 / 3")]
    [TestCase("2 / x")]
    public void Process_NoError(string expression)
    {
        var function = ParseWithoutProcessing(expression);

        new DivideByZeroValidator().Process(function);

        function.ThisAndDescendentsWithErrors.Should().BeEmpty();
    }

    [Test]
    public void Process_Error()
    {
        var function = ParseWithoutProcessing("2 / 0");
        new DivideByZeroValidator().Process(function);

        var errors = function.ThisAndDescendentsWithErrors.ToList();
        errors.Should().HaveCount(1);

        var @operator = errors[0].Should().BeOfType<BinaryOperation>().Subject;
        @operator.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        @operator.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(0);
        @operator.Errors.Should().BeEquivalentTo(new[] { Message.Error("Divide by zero.") });
    }
}