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

        pipeline.Run(N1).Success.Should().Equal(lastStageShouldContinue);

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

        var (success, _, lastStageRan) = pipeline.Run(N1);
        success.Should().Equal(lastStageShouldContinue);
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

        var (success, _, lastStageRan) = pipeline.Run(N1);
        success.Should().BeFalse();
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

        pipeline.Run(N1).Success.Should().BeFalse();

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void Run_Tuple_ReplacesRoot()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer(replacement);
        var processor = new TestProcessor();

        var stages = new PipelineStage<TestNode>[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]),
            new SerialPipelineStage<TestNode>("Stage 2", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        var (success, root, lastStageRun) = pipeline.Run(N1);
        success.Should().BeTrue();
        root.Should().BeTheSameInstanceAs(replacement);
        lastStageRun.Should().Equal("Stage 2");
    }

    [Test]
    public void Run_Out_ReplacesRoot()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer(replacement);

        var stages = new PipelineStage<TestNode>[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var newRoot).Should().BeTrue();
        newRoot.Should().BeTheSameInstanceAs(replacement);
    }

    [Test]
    public void Run_Out_LastStageRun_ReplacesRoot()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer(replacement);

        var stages = new PipelineStage<TestNode>[]
        {
            new SerialPipelineStage<TestNode>("Stage 1", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer])
        };

        var pipeline = new Pipeline<TestNode>(stages);

        pipeline.Run(N1, out var newRoot, out var lastStageRun).Should().BeTrue();
        newRoot.Should().BeTheSameInstanceAs(replacement);
        lastStageRun.Should().Equal("Stage 1");
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

        pipeline.Run(context, N1).Success.Should().Equal(lastStageShouldContinue);

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

        var (success, _, lastStageRan) = pipeline.Run(context, N1);
        success.Should().Equal(lastStageShouldContinue);
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

        var (success, _, lastStageRan) = pipeline.Run(context, N1);
        success.Should().BeFalse();
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

        pipeline.Run(context, N1).Success.Should().BeFalse();

        processor1.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor2.Processed.Should().BeEmpty();
    }

    [Test]
    public void WithContext_Run_Tuple_ReplacesRoot()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer<object>(context, replacement);

        var stages = new PipelineStage<object, TestNode>[]
        {
            new SerialPipelineStage<object, TestNode>("Stage 1", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        var (success, root, lastStageRun) = pipeline.Run(context, N1);
        success.Should().BeTrue();
        root.Should().BeTheSameInstanceAs(replacement);
        lastStageRun.Should().Equal("Stage 1");
    }

    [Test]
    public void WithContext_Run_Out_ReplacesRoot()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer<object>(context, replacement);

        var stages = new PipelineStage<object, TestNode>[]
        {
            new SerialPipelineStage<object, TestNode>("Stage 1", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1, out var newRoot).Should().BeTrue();
        newRoot.Should().BeTheSameInstanceAs(replacement);
    }

    [Test]
    public void WithContext_Run_Out_LastStageRun_ReplacesRoot()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer<object>(context, replacement);

        var stages = new PipelineStage<object, TestNode>[]
        {
            new SerialPipelineStage<object, TestNode>("Stage 1", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer])
        };

        var pipeline = new Pipeline<object, TestNode>(stages);

        pipeline.Run(context, N1, out var newRoot, out var lastStageRun).Should().BeTrue();
        newRoot.Should().BeTheSameInstanceAs(replacement);
        lastStageRun.Should().Equal("Stage 1");
    }

    private sealed class TestRootReplacer(TestNode replacement) : Replacer<TestNode>
    {
        protected override TestNode? Replace(TestNode node) => node.HasParent ? node : replacement;
    }

    private sealed class TestRootReplacer<TContext>(TContext expectedContext, TestNode replacement) : Replacer<TContext, TestNode>
    {
        protected override TestNode? Replace(TContext context, TestNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return node.HasParent ? node : replacement;
        }
    }
}
