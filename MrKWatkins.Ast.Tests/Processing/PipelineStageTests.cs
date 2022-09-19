using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineStageTests : TreeTestFixture
{
    [Test]
    public void Constructor_ThrowsForNoStages() =>
        FluentActions.Invoking(() => new PipelineStage<TestNode>("Test Stage", Array.Empty<Processor<TestNode>>(), _ => true))
            .Should().Throw<ArgumentException>()
            .WithParameters("Value is empty.", "processors");

    [Test]
    public void Run([Values(true, false)] bool shouldContinue)
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        bool ShouldContinue(TestNode root)
        {
            root.Should().BeSameAs(N1);
            return shouldContinue;
        }

        var stage = new PipelineStage<TestNode>("Test Stage", processors, ShouldContinue);
        stage.Name.Should().Be("Test Stage");

        stage.Run(N1).Should().Be(shouldContinue);
        processors[0].Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1));
        processors[1].Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_ProcessorThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processors = new[]
        {
            new TestProcessor(),
            new TestProcessor { ProcessNodeOverride = n => n == N123 ? throw exception : null }
        };

        var stage = new PipelineStage<TestNode>("Test Stage", processors, _ => true);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>()
            .WithParameters("Exception occurred executing processor TestProcessor.", "Test Stage")
            .WithInnerException<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N123)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Run_ShouldContinueThrows()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        var exception = new InvalidOperationException("Test");

        var stage = new PipelineStage<TestNode>("Test Stage", processors, _ => throw exception);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>()
            .WithParameters("Exception occurred executing should continue function.", "Test Stage")
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }
}