using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineTests : TreeTestFixture
{
    [Test]
    public void Constructor_ThrowsForNoStages() =>
        FluentActions.Invoking(() => new Pipeline<TestNode>(Array.Empty<PipelineStage<TestNode>>()))
            .Should().Throw<ArgumentException>()
            .WithParameters("Value is empty.", "stages");

    [Test]
    public void Run_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var processor1 = new TestUnorderedProcessor();
        var processor2 = new TestUnorderedProcessor();

        var stages = new[]
        {
            new PipelineStage<TestNode>("Stage 1", [processor1], _ => true),
            new PipelineStage<TestNode>("Stage 2", [processor2], _ => lastStageShouldContinue)
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1).Should().Be(lastStageShouldContinue);

        processor1.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_LastStageRan_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var processor1 = new TestUnorderedProcessor();
        var processor2 = new TestUnorderedProcessor();

        var stages = new[]
        {
            new PipelineStage<TestNode>("Stage 1", [processor1], _ => true),
            new PipelineStage<TestNode>("Stage 2", [processor2], _ => lastStageShouldContinue)
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var lastStageRan).Should().Be(lastStageShouldContinue);
        lastStageRan.Should().Be("Stage 2");

        processor1.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_SomeStages()
    {
        var processor1 = new TestUnorderedProcessor();
        var processor2 = new TestUnorderedProcessor();

        var stages = new[]
        {
            new PipelineStage<TestNode>("Stage 1", [processor1], _ => false),
            new PipelineStage<TestNode>("Stage 2", [processor2], _ => true)
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1).Should().BeFalse();

        processor1.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void Run_LastStageRan_AllStages()
    {
        var processor1 = new TestUnorderedProcessor();
        var processor2 = new TestUnorderedProcessor();

        var stages = new[]
        {
            new PipelineStage<TestNode>("Stage 1", [processor1], _ => false),
            new PipelineStage<TestNode>("Stage 2", [processor2], _ => true)
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var lastStageRan).Should().BeFalse();
        lastStageRan.Should().Be("Stage 1");

        processor1.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }
}