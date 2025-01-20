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

        var @operator = errors[0].Should().BeOfType<BinaryOperation>().Value;
        @operator.Left.Should().BeOfType<Constant>().That.Value.Should().Equal(2);
        @operator.Right.Should().BeOfType<Constant>().That.Value.Should().Equal(0);
        @operator.Errors.Should().SequenceEqual(Message.Error("Divide by zero."));
    }
}