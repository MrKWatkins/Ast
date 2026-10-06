using System.Collections.Concurrent;
using System.Diagnostics;
using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelPipelineStageTests : TreeTestFixture
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

    [Test]
    public void Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [], Environment.ProcessorCount, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void Constructor_ThrowsForOrderedProcessor() =>
        AssertThat
            .Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestOrderedProcessor()], Environment.ProcessorCount, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("OrderedProcessors cannot be used in a parallel stage.").And
            .HaveParamName("processors");

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_ThrowsForInvalidMaxDegreeOfParallelism(int maxDegreeOfParallelism) =>
        AssertThat.Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor()], maxDegreeOfParallelism, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value must be greater than 0.").And
            .HaveParamName("maxDegreeOfParallelism");

    [Test]
    public void Constructor_ThrowsForInvalidStrategy() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor()], Environment.ProcessorCount, (ParallelStrategy) 123))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is not a valid strategy.").And
            .HaveParamName("strategy");

    [Test]
    public void Run([Values(true, false)] bool shouldContinue, [Values] ParallelStrategy strategy)
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        bool ShouldContinue(TestNode root)
        {
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, strategy);
        stage.Name.Should().Equal("Test Stage");
        stage.Strategy.Should().Equal(strategy);

        stage.Run(N1).Success.Should().Equal(shouldContinue);
        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
    }

    [Test]
    public void Run_PerNode_ProcessorsRunInOrderOnOneThreadForEachNode()
    {
        // Record the processor index and thread that processed each node, in order.
        var processedBy = new ConcurrentDictionary<TestNode, ConcurrentQueue<(int Processor, int Thread)>>();

        var processors = Enumerable.Range(0, 3)
            .Select(index => new TestProcessor
            {
                ProcessNodeOverride = node =>
                {
                    processedBy.GetOrAdd(node, _ => new ConcurrentQueue<(int, int)>()).Enqueue((index, Environment.CurrentManagedThreadId));
                    Thread.Sleep(1); // Widen the window for another thread to break in if the guarantee did not hold.
                }
            })
            .ToArray();

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerNode);

        stage.Run(N1);

        processedBy.Keys.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        foreach (var record in processedBy.Values)
        {
            // Every processor ran, in registration order, on one thread.
            record.Select(r => r.Processor).Should().SequenceEqual(0, 1, 2);
            record.Select(r => r.Thread).Distinct().Should().HaveCount(1);
        }
    }

    [Test]
    public void Run_PerNode_NodesAreProcessedConcurrently()
    {
        (Environment.ProcessorCount > 1).Should().BeTrue();

        // The first node to be processed waits for a second node to start being processed. If nodes were processed
        // one at a time the wait would time out.
        var arrivals = 0;
        using var secondArrived = new ManualResetEventSlim();
        var sawConcurrency = false;

        var processor = new TestProcessor
        {
            ProcessNodeOverride = _ =>
            {
                if (Interlocked.Increment(ref arrivals) == 1)
                {
                    sawConcurrency = secondArrived.Wait(Timeout);
                }
                else
                {
                    secondArrived.Set();
                }
            }
        };

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [processor], Environment.ProcessorCount, ParallelStrategy.PerNode);

        stage.Run(N1);

        sawConcurrency.Should().BeTrue();
        processor.Processed.Should().HaveCount(NodeCount);
    }

    [Test]
    public async Task Run_PerNode_NodeIsNotProcessedByTwoProcessorsAtOnce()
    {
        (Environment.ProcessorCount > 1).Should().BeTrue();

        // processors[0] blocks on the root. processors[1] should process every other node but not the root, as per node
        // means the root is held by processors[0] until it is unblocked.
        var block = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var processors = new[]
        {
            new TestProcessor
            {
                ProcessNodeOverride = node =>
                {
                    if (node == N1)
                    {
                        block.Task.Wait(Timeout).Should().BeTrue();
                    }
                }
            },
            new TestProcessor()
        };

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerNode);

        var runTask = Task.Run(() => stage.Run(N1));

        await WaitUntil(() => processors[1].Processed.Count() == NodeCount - 1);

        processors[1].Processed.Contains(N1).Should().BeFalse();
        processors[0].Processed.Contains(N1).Should().BeFalse();

        block.SetResult();
        await runTask.WaitAsync(Timeout);

        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
    }

    [Test]
    public void Run_PerProcessor_EachProcessorWalksTheWholeTreeInOrderOnOneThread()
    {
        var threads = new ConcurrentDictionary<int, ConcurrentBag<int>>();

        var processors = Enumerable.Range(0, 3)
            .Select(index => new TestProcessor
            {
                ProcessNodeOverride = _ => threads.GetOrAdd(index, _ => []).Add(Environment.CurrentManagedThreadId)
            })
            .ToArray();

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerProcessor);

        stage.Run(N1);

        for (var index = 0; index < processors.Length; index++)
        {
            // Each processor sees the nodes in traversal order, as it has its own walk of the tree, all on one thread.
            processors[index].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
            threads[index].Distinct().Should().HaveCount(1);
        }
    }

    [Test]
    public async Task Run_PerProcessor_ProcessorsRunConcurrently()
    {
        (Environment.ProcessorCount > 1).Should().BeTrue();

        // processors[0] blocks on every node, so cannot get past the root. processors[1] should process the whole tree
        // regardless, including the root that processors[0] is blocked on.
        var block = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var processors = new[]
        {
            new TestProcessor { ProcessNodeOverride = _ => block.Task.Wait(Timeout).Should().BeTrue() },
            new TestProcessor()
        };

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerProcessor);

        var runTask = Task.Run(() => stage.Run(N1));

        await WaitUntil(() => processors[1].Processed.Count() == NodeCount);

        processors[0].Processed.Should().BeEmpty();

        block.SetResult();
        await runTask.WaitAsync(Timeout);

        processors[0].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Run_ProcessorThrows([Values] ParallelStrategy strategy)
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

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, strategy);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_TraversalThrowsLazily([Values] ParallelStrategy strategy)
    {
        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, new ThrowingTraversal(exception), [new TestProcessor()], Environment.ProcessorCount, strategy);

        var pipelineException = stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That;

        // The exception happens inside Parallel.ForEach, so is wrapped in an AggregateException.
        pipelineException.Should().HaveParameters("Exception occurred traversing the tree.", "Test Stage");
        pipelineException.InnerException.Should().BeOfType<AggregateException>().Value.InnerExceptions.Should().SequenceEqual(exception);
    }

    [Test]
    public void Run_TraversalThrowsEagerly()
    {
        var exception = new InvalidOperationException("Test");

        // Per node enumerates the tree before calling Parallel.ForEach, so an eager exception is not wrapped in an AggregateException.
        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => true, new ThrowingTraversal(exception, lazily: false), [new TestProcessor()], Environment.ProcessorCount, ParallelStrategy.PerNode);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred traversing the tree.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void Run_ShouldContinueThrows()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };

        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<TestNode>("Test Stage", _ => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerNode);

        stage.Invoking(s => s.Run(N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing the should continue function.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Constructor_ThrowsForNoStages() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [], Environment.ProcessorCount, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is empty.").And
            .HaveParamName("processors");

    [Test]
    public void WithContext_Constructor_ThrowsForOrderedProcessor() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>(
                "Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestOrderedProcessor<object>(new object())], Environment.ProcessorCount, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("OrderedProcessors cannot be used in a parallel stage.").And
            .HaveParamName("processors");

    [TestCase(0)]
    [TestCase(-1)]
    public void WithContext_Constructor_ThrowsForInvalidMaxDegreeOfParallelism(int maxDegreeOfParallelism) =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>(
                "Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor<object>()], maxDegreeOfParallelism, ParallelStrategy.PerNode))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value must be greater than 0.").And
            .HaveParamName("maxDegreeOfParallelism");

    [Test]
    public void WithContext_Constructor_ThrowsForInvalidStrategy() =>
        AssertThat.Invoking(() => new ParallelPipelineStage<object, TestNode>(
                "Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [new TestProcessor<object>()], Environment.ProcessorCount, (ParallelStrategy) 123))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is not a valid strategy.").And
            .HaveParamName("strategy");

    [Test]
    public void WithContext_Run([Values(true, false)] bool shouldContinue, [Values] ParallelStrategy strategy)
    {
        var context = new object();
        var processors = new[] { new TestProcessor<object>(context), new TestProcessor<object>(context) };

        bool ShouldContinue(object actualContext, TestNode root)
        {
            actualContext.Should().BeTheSameInstanceAs(context);
            root.Should().BeTheSameInstanceAs(N1);
            return shouldContinue;
        }

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", ShouldContinue, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, strategy);
        stage.Name.Should().Equal("Test Stage");
        stage.Strategy.Should().Equal(strategy);

        stage.Run(context, N1).Success.Should().Equal(shouldContinue);
        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
    }

    [Test]
    public async Task WithContext_Run_PerNode_NodeIsNotProcessedByTwoProcessorsAtOnce()
    {
        (Environment.ProcessorCount > 1).Should().BeTrue();

        var context = new object();
        var block = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var processors = new[]
        {
            new TestProcessor<object>(context)
            {
                ProcessNodeOverride = node =>
                {
                    if (node == N1)
                    {
                        block.Task.Wait(Timeout).Should().BeTrue();
                    }
                }
            },
            new TestProcessor<object>(context)
        };

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerNode);

        var runTask = Task.Run(() => stage.Run(context, N1));

        await WaitUntil(() => processors[1].Processed.Count() == NodeCount - 1);

        processors[1].Processed.Contains(N1).Should().BeFalse();
        processors[0].Processed.Contains(N1).Should().BeFalse();

        block.SetResult();
        await runTask.WaitAsync(Timeout);

        processors[0].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
        processors[1].Processed.OrderBy(n => n.Name).Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OrderBy(n => n.Name));
    }

    [Test]
    public async Task WithContext_Run_PerProcessor_ProcessorsRunConcurrently()
    {
        (Environment.ProcessorCount > 1).Should().BeTrue();

        var context = new object();
        var block = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var processors = new[]
        {
            new TestProcessor<object>(context) { ProcessNodeOverride = _ => block.Task.Wait(Timeout).Should().BeTrue() },
            new TestProcessor<object>(context)
        };

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerProcessor);

        var runTask = Task.Run(() => stage.Run(context, N1));

        await WaitUntil(() => processors[1].Processed.Count() == NodeCount);

        processors[0].Processed.Should().BeEmpty();

        block.SetResult();
        await runTask.WaitAsync(Timeout);

        processors[0].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processors[1].Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void WithContext_Run_ProcessorThrows([Values] ParallelStrategy strategy)
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

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, strategy);

        stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That.Should()
            .HaveParameters("Exception occurred executing processor TestProcessor<Object> for node N123.", "Test Stage").And
            .HaveInnerException(exception);
    }

    [Test]
    public void WithContext_Run_TraversalThrowsLazily([Values] ParallelStrategy strategy)
    {
        var context = new object();
        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => true, new ThrowingTraversal(exception), [new TestProcessor<object>(context)], Environment.ProcessorCount, strategy);

        var pipelineException = stage.Invoking(s => s.Run(context, N1))
            .Should().Throw<PipelineException>().That;

        pipelineException.Should().HaveParameters("Exception occurred traversing the tree.", "Test Stage");
        pipelineException.InnerException.Should().BeOfType<AggregateException>().Value.InnerExceptions.Should().SequenceEqual(exception);
    }

    [Test]
    public void WithContext_Run_ShouldContinueThrows()
    {
        var context = new object();
        var processors = new[] { new TestProcessor<object>(context), new TestProcessor<object>(context) };

        var exception = new InvalidOperationException("Test");

        var stage = new ParallelPipelineStage<object, TestNode>("Test Stage", (_, _) => throw exception, DepthFirstPreOrderTraversal<TestNode>.Instance, processors, Environment.ProcessorCount, ParallelStrategy.PerNode);

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