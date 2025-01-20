using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineStageTests : TreeTestFixture
{
    [Test]
    public void Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new PipelineStage<TestNode>("Test Stage", Array.Empty<Processor<TestNode>>(), _ => true))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void Run([Values(true, false)] bool shouldContinue)
    {
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        bool ShouldContinue(TestNode root)
        {
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new PipelineStage<TestNode>("Test Stage", processors, ShouldContinue);
        stage.Name.Should().Equal("Test Stage");

        stage.Run(N1).Should().Equal(shouldContinue);
        processors[0].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processors[1].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_ProcessorThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processors = new[]
        {
            new TestUnorderedProcessor(),
            new TestUnorderedProcessor { ProcessNodeOverride = n => n == N123 ? throw exception : null }
        };

        var stage = new PipelineStage<TestNode>("Test Stage", processors, _ => true);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestUnorderedProcessor.", "Test Stage").And
            .HaveInnerException<AggregateException>().That.Should()
            .HaveInnerException<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during ProcessNode.", N123).And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_ShouldContinueThrows()
    {
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var exception = new InvalidOperationException("Test");

        var stage = new PipelineStage<TestNode>("Test Stage", processors, _ => throw exception);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }
}