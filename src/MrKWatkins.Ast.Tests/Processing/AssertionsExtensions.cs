using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

[PublicAPI]
public static class AssertionsExtensions
{
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