using System.Diagnostics;
using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelPipelineStageTests : TreeTestFixture
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

    [Test]
    public void Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [], Environment.ProcessorCount))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void Constructor_ThrowsForOrderedProcessor() =>
        AssertThat
            .Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestOrderedProcessor()], Environment.ProcessorCount))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("OrderedProcessors cannot be used in a parallel stage.").And
            .HaveParamName("processors");

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_ThrowsForInvalidMaxDegreeOfParallelism(int maxDegreeOfParallelism) =>
        AssertThat.Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor()], maxDegreeOfParallelism))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value must be greater than 0.").And
            .HaveParamName("maxDegreeOfParallelism");

    [Test]
    public void Run([Values(true, false)] bool shouldContinue)
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        bool ShouldContinue(TestNode root)
        {
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);
        stage.Name.Should().Equal("Test Stage");

        stage.Run(N1).Should().Equal(shouldContinue);
        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
    }

    [Test]
    public async Task Run_ProcessesInParallel()
    {
        // Use a small tree to avoid blocking all threads on the build server.
        (Environment.ProcessorCount > 3).Should().BeTrue();
        var tree = new ANode(new ANode());

        var block = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var processors = new[]
        {
            new TestProcessor { ProcessNodeOverride = _ => block.Task.Wait() },
            new TestProcessor()
        };

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);
        stage.Name.Should().Equal("Test Stage");

        var runTask = Task.Run(() => stage.Run(tree));

        // processors[0] blocks. Wait until processors[1] is complete.
        await WaitUntil(() => processors[1].Processed.Count() == 2);

        // processors[1] should still be empty.
        processors[0].Processed.Should().BeEmpty();

        // Unblock; processors[1] should complete.
        block.SetResult();
        await WaitUntil(() => processors[1].Processed.Count() == 2);

        await runTask.WaitAsync(Timeout);
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

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_ShouldContinueThrows()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing the should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [], Environment.ProcessorCount))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void WithContext_Constructor_ThrowsForOrderedProcessor() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>(
                "Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestOrderedProcessor<object>(new object())], Environment.ProcessorCount))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("OrderedProcessors cannot be used in a parallel stage.").And
            .HaveParamName("processors");

    [TestCase(0)]
    [TestCase(-1)]
    public void WithContext_Constructor_ThrowsForInvalidMaxDegreeOfParallelism(int maxDegreeOfParallelism) =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>(
                "Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor<object>()], maxDegreeOfParallelism))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value must be greater than 0.").And
            .HaveParamName("maxDegreeOfParallelism");

    [Test]
    public void WithContext_Run([Values(true, false)] bool shouldContinue)
    {
        var context = new object();
        var processors = new[] { new TestProcessor<object>(context), new TestProcessor<object>(context) };

        bool ShouldContinue(object actualContext, TestNode root)
        {
            actualContext.Should().BeTheSameInstanceAs(context);
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);
        stage.Name.Should().Equal("Test Stage");

        stage.Run(context, N1).Should().Equal(shouldContinue);
        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
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

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor<Object> for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Run_ShouldContinueThrows()
    {
        var context = new object();
        var processors = new[] { new TestProcessor<object>(context), new TestProcessor<object>(context) };

        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing the should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }

    private static async Task WaitUntil(Func<bool> predicate)
    {
        var stopwatch = Stopwatch.StartNew();
        while (true)
        {
            if (predicate())
            {
                break;
            }

            if (stopwatch.Elapsed > Timeout)
            {
                throw new TimeoutException("Action did not complete after 10 seconds.");
            }

            await Task.Delay(100);
        }
    }
}