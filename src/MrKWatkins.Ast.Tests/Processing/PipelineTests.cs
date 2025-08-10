using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineTests : TreeTestFixture
{
    [Test]
    public void Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new Pipeline<TestNode>([]))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("stages");

    [Test]
    public void Run_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var processor1 = new TestProcessor();
        var processor2 = new TestProcessor();

        var stages = new[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<TestNode>("Stage 2", _ => lastStageShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1).Should().Equal(lastStageShouldContinue);

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_LastStageRan_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var processor1 = new TestProcessor();
        var processor2 = new TestProcessor();

        var stages = new[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<TestNode>("Stage 2", _ => lastStageShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var lastStageRan).Should().Equal(lastStageShouldContinue);
        lastStageRan.Should().Equal("Stage 2");

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_LastStageRan_SomeStages()
    {
        var processor1 = new TestProcessor();
        var processor2 = new TestProcessor();

        var stages = new[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => false, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<TestNode>("Stage 2", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var lastStageRan).Should().BeFalse();
        lastStageRan.Should().Equal("Stage 1");

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void Run_SomeStages()
    {
        var processor1 = new TestProcessor();
        var processor2 = new TestProcessor();

        var stages = new[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => false, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<TestNode>("Stage 2", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1).Should().BeFalse();

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void WithContext_Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new Pipeline<object, TestNode>([]))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("stages");

    [Test]
    public void WithContext_Run_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var context = new object();
        var processor1 = new TestProcessor<object>(context);
        var processor2 = new TestProcessor<object>(context);

        var stages = new[]
        {
            new SerialPipelineStage<object, TestNode>(
                "Stage 1", (c, _) =>
                {
                    c.Should().BeTheSameInstanceAs(context);
                    return true;
                }, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<object, TestNode>(
                "Stage 2", (c, _) =>
                {
                    c.Should().BeTheSameInstanceAs(context);
                    return lastStageShouldContinue;
                }, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1).Should().Equal(lastStageShouldContinue);

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void WithContext_Run_LastStageRan_AllStages([Values(true, false)] bool lastStageShouldContinue)
    {
        var context = new object();
        var processor1 = new TestProcessor<object>(context);
        var processor2 = new TestProcessor<object>(context);

        var stages = new[]
        {
            new SerialPipelineStage<object, TestNode>(
                "Stage 1", (c, _) =>
                {
                    c.Should().BeTheSameInstanceAs(context);
                    return true;
                }, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<object, TestNode>(
                "Stage 2", (c, _) =>
                {
                    c.Should().BeTheSameInstanceAs(context);
                    return lastStageShouldContinue;
                }, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1, out var lastStageRan).Should().Equal(lastStageShouldContinue);
        lastStageRan.Should().Equal("Stage 2");

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void WithContext_Run_LastStageRan_SomeStages()
    {
        var context = new object();
        var processor1 = new TestProcessor<object>(context);
        var processor2 = new TestProcessor<object>(context);

        var stages = new[]
        {
            new SerialPipelineStage<object, TestNode>("Stage 1", (_, _) => false, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<object, TestNode>("Stage 2", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1, out var lastStageRan).Should().BeFalse();
        lastStageRan.Should().Equal("Stage 1");

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void WithContext_Run_SomeStages()
    {
        var context = new object();
        var processor1 = new TestProcessor<object>(context);
        var processor2 = new TestProcessor<object>(context);

        var stages = new[]
        {
            new SerialPipelineStage<object, TestNode>("Stage 1", (_, _) => false, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor1]),
            new SerialPipelineStage<object, TestNode>("Stage 2", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor2])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1).Should().BeFalse();

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }
}