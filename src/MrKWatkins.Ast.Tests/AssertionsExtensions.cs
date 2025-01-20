using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests;

// TODO: Restore scope.
[PublicAPI]
public static class AssertionsExtensions
{
    [Pure]
    public static ExceptionAssertions<ProcessingException<TestNode>> Should(this ProcessingException<TestNode>? value) => new(value);

    public static ExceptionAssertionsChain<ProcessingException<TestNode>> HaveParameters(
        this ExceptionAssertions<ProcessingException<TestNode>> assertions,
        string expectedMessage, TestNode expectedNode)
    {
        assertions.Value.Should().HaveMessage($"{expectedMessage} (Node '{expectedNode}')");
        assertions.Value.Node.Should().Equal(expectedNode);

        return new ExceptionAssertionsChain<ProcessingException<TestNode>>(assertions);
    }

    [Pure]
    public static ExceptionAssertions<PipelineException> Should(this PipelineException? value) => new(value);

    public static ExceptionAssertionsChain<PipelineException> HaveParameters(
        this ExceptionAssertions<PipelineException> assertions,
        string expectedMessage, string expectedStage)
    {
        assertions.Value.Should().HaveMessage($"{expectedMessage} (Stage '{expectedStage}')");
        assertions.Value.Stage.Should().Equal(expectedStage);

        return new ExceptionAssertionsChain<PipelineException>(assertions);
    }
}