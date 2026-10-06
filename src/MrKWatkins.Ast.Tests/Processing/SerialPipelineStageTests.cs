using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class SerialPipelineStageTests : TreeTestFixture
{
    [Test]
    public void Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, []))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void Run([Values(true, false)] bool shouldContinue)
    {
        var processor = new TestProcessor();
        var orderedProcessor = new TestOrderedProcessor();
        var orderedProcessorShouldProcessDescendantsOverride = new TestOrderedProcessor { ShouldProcessDescendantsOverride = n => n == N1 };
        var orderedProcessorTraversalOverride = new TestOrderedProcessor { TraversalOverride = BreadthFirstTraversal<TestNode>.Instance };

        bool ShouldContinue(TestNode root)
        {
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new SerialPipelineStage<TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor, orderedProcessor, orderedProcessorShouldProcessDescendantsOverride, orderedProcessorTraversalOverride]);
        stage.Name.Should().Equal("Test Stage");

        stage.Run(N1).Success.Should().Equal(shouldContinue);
        processor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        orderedProcessor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        orderedProcessorShouldProcessDescendantsOverride.Processed.Should().SequenceEqual(N1, N11, N12, N13);
        orderedProcessorTraversalOverride.Processed.Should().SequenceEqual(TestNode.Traverse.BreadthFirst(N1));
    }

    [Test]
    public void Run_ProcessorThrows()
    {
        var exception = new InvalidOperationException("Test");

        void ProcessNodeOverride(TestNode n)
        {
            if (n == N123)
            {
                throw exception;
            }
        }

        var processors = new[]
        {
            new TestProcessor(),
            new TestProcessor { ProcessNodeOverride = ProcessNodeOverride }
        };

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_ShouldContinueThrows()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        var exception = new InvalidOperationException("Test");

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing the should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_Deconstruct_ReplacesRoot([Values(true, false)] bool shouldContinue)
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer(replacement);

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => shouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var (success, root) = stage.Run(N1);
        success.Should().Equal(shouldContinue);
        root.Should().BeTheSameInstanceAs(replacement);
    }

    [Test]
    public void Run_Result_NoRootReplacement()
    {
        var processor = new TestProcessor();

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor]);

        var result = stage.Run(N1);
        result.Success.Should().BeTrue();
        result.Root.Should().BeTheSameInstanceAs(N1);
    }

    [Test]
    public void Run_TraversalThrows()
    {
        var exception = new InvalidOperationException("Test");

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, new ThrowingTraversal(exception), [new TestProcessor()]);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_OrderedProcessorTraversalThrows()
    {
        var exception = new InvalidOperationException("Test");

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestOrderedProcessor { TraversalOverride = new ThrowingTraversal(exception) }]);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestOrderedProcessor.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, []))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void WithContext_Run([Values(true, false)] bool shouldContinue)
    {
        var context = new object();
        var processor = new TestProcessor<object>(context);
        var orderedProcessor = new TestOrderedProcessor<object>(context);
        var orderedProcessorShouldProcessDescendantsOverride = new TestOrderedProcessor<object>(context) { ShouldProcessDescendantsOverride = n => n == N1 };
        var orderedProcessorTraversalOverride = new TestOrderedProcessor<object>(context) { TraversalOverride = BreadthFirstTraversal<TestNode>.Instance };

        bool ShouldContinue(object actualContext, TestNode root)
        {
            actualContext.Should().BeTheSameInstanceAs(context);
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor, orderedProcessor, orderedProcessorShouldProcessDescendantsOverride, orderedProcessorTraversalOverride]);
        stage.Name.Should().Equal("Test Stage");

        stage.Run(context, N1).Success.Should().Equal(shouldContinue);
        processor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        orderedProcessor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        orderedProcessorShouldProcessDescendantsOverride.Processed.Should().SequenceEqual(N1, N11, N12, N13);
        orderedProcessorTraversalOverride.Processed.Should().SequenceEqual(TestNode.Traverse.BreadthFirst(N1));
    }

    [Test]
    public void WithContext_Run_ProcessorThrows()
    {
        var context = new object();
        var exception = new InvalidOperationException("Test");

        void ProcessNodeOverride(TestNode n)
        {
            if (n == N123)
            {
                throw exception;
            }
        }

        var processors = new[]
        {
            new TestProcessor<object>(context),
            new TestProcessor<object>(context) { ProcessNodeOverride = ProcessNodeOverride }
        };

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor<Object> for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Run_TraversalThrows()
    {
        var context = new object();
        var exception = new InvalidOperationException("Test");

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => true, new ThrowingTraversal(exception), [new TestProcessor<object>(context)]);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor<Object>.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Run_ShouldContinueThrows()
    {
        var context = new object();
        var processors = new[] { new TestProcessor<object>(context), new TestProcessor<object>(context) };

        var exception = new InvalidOperationException("Test");

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing the should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Run_Deconstruct_ReplacesRoot([Values(true, false)] bool shouldContinue)
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestRootReplacer<object>(context, replacement);

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => shouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var (success, root) = stage.Run(context, N1);
        success.Should().Equal(shouldContinue);
        root.Should().BeTheSameInstanceAs(replacement);
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