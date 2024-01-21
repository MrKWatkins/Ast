using System.Collections;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelProcessorTests : TreeTestFixture
{
    [Test]
    public void Constructor_ProcessorsEmptyThrows() =>
        FluentActions.Invoking(() => new ParallelProcessor<TestNode>(Enumerable.Empty<Processor<TestNode>>(), 5))
            .Should().Throw<ArgumentException>()
            .WithParameters("Value is empty.", "processors");

    [TestCase(0)]
    [TestCase(-1)]
    [SuppressMessage("Maintainability", "CA1507:Use nameof in place of string", Justification = "Name coincidentally is shared with parameter.")]
    public void Constructor_InvalidMaxDegreeOfParallelismThrows(int maxDegreeOfParallelism) =>
        FluentActions.Invoking(() => new ParallelProcessor<TestNode>(new List<Processor<TestNode>> { new TestValidator() }, maxDegreeOfParallelism))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithParameters("maxDegreeOfParallelism", maxDegreeOfParallelism, "Value must be greater than 0.");

    [Test]
    public void Constructor()
    {
        var processor = new ParallelProcessor<TestNode>(new List<Processor<TestNode>> { new TestValidator(), new TestValidator() }, 5);
        processor.MaxDegreeOfParallelism.Should().Be(5);
    }

    [TestCase(1)]
    [TestCase(2)]
    public void Process(int maxDegreeOfParallelism)
    {
        var validator1 = new TestValidator
        {
            { N12, () => "N12 Error" },
            { N121, () => "N121 Error" }
        };
        var validator2 = new TestValidator
        {
            { N121, () => "N121 Error" },
            { N123, () => "N123 Error" }
        };

        var parallel = new ParallelProcessor<TestNode>(new[] { validator1, validator2 }, maxDegreeOfParallelism);

        parallel.Process(N1);

        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121, N123 });

        N12.Messages.Should().BeEquivalentTo(new[] { Message.Error("N12 Error") });

        // Note that without the lock in Node<TNode>.AddMessage this contains just one message a good percentage of the time.
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Error("N121 Error"), Message.Error("N121 Error") });
        N123.Messages.Should().BeEquivalentTo(new[] { Message.Error("N123 Error") });
    }

    [TestCase(1)]
    [TestCase(2)]
    public void Process_ValidatorsThrow(int maxDegreeOfParallelism)
    {
        var n1Exception = new InvalidOperationException("N121 Exception");
        var validator1 = new TestValidator
        {
            // Should still continue on and process other nodes; annoying to debug one exception, retry and have another, etc. Return the lot.
            { N1, () => throw n1Exception },
            { N12, () => "N12 Error" },
            { N121, () => "N121 Error" }
        };
        var n121Exception = new InvalidOperationException("N121 Exception");
        var validator2 = new TestValidator
        {
            { N121, () => throw n121Exception },
            { N123, () => "N123 Error" }
        };

        var parallel = new ParallelProcessor<TestNode>(new[] { validator1, validator2 }, maxDegreeOfParallelism);

        var innerExceptions = parallel.Invoking(p => p.Process(N1)).Should().Throw<AggregateException>().Which.InnerExceptions;
        innerExceptions.Should().HaveCount(2);

        var processingExceptions = innerExceptions.OfType<ProcessingException<TestNode>>().ToList();
        processingExceptions.Should().HaveCount(2);
        processingExceptions.Should().ContainSingle(e => e.Node == N1).Which
            .InnerException.Should().Be(n1Exception);
        processingExceptions.Should().ContainSingle(e => e.Node == N121).Which
            .InnerException.Should().Be(n121Exception);

        // The non-throwing validations in the tree should still have been processed.
        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121, N123 });

        N12.Messages.Should().BeEquivalentTo(new[] { Message.Error("N12 Error") });
        // Only one because the other threw.
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Error("N121 Error") });
        N123.Messages.Should().BeEquivalentTo(new[] { Message.Error("N123 Error") });
    }

    [Test]
    [SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "Not relevant in test code.")]
    public async Task Process_RunsInParallel()
    {
        var validator1LastNodeCalled = new TaskCompletionSource();
        var validator1 = new TestValidator
        {
            { N12, () => "N12 Error" },
            {
                N121, () =>
                {
                    validator1LastNodeCalled.SetResult();
                    return "N121 Error";
                }
            }
        };

        var validator2FirstNodeCalled = new TaskCompletionSource();
        var validator2FirstNodeBlock = new TaskCompletionSource();
        var validator2LastNodeCalled = new TaskCompletionSource();
        var validator2 = new TestValidator
        {
            {
                N121, () =>
                {
                    validator2FirstNodeCalled.SetResult();
                    validator2FirstNodeBlock.Task.Wait();
                    return "N121 Error";
                }
            },
            {
                N123, () =>
                {
                    validator2LastNodeCalled.SetResult();
                    return "N123 Error";
                }
            }
        };

        var parallel = new ParallelProcessor<TestNode>(new[] { validator1, validator2 }, 3);

        var processTask = Task.Run(() => parallel.Process(N1));

        // Wait until validator2 is blocking on N121.
        await validator2FirstNodeCalled.Task.WaitAsync(TimeSpan.FromSeconds(10));

        // Both the validator1 and the subsequent validation in validator2 should be able to proceed as MaxDegreeOfParallelism is 3,
        // allowing our block to happen and at least 2 other tasks to proceed.
        await validator1LastNodeCalled.Task.WaitAsync(TimeSpan.FromSeconds(10));
        await validator2LastNodeCalled.Task.WaitAsync(TimeSpan.FromSeconds(10));

        // Should still be processing.
        processTask.IsCompleted.Should().BeFalse();

        // Unblock the task and wait to finish.
        validator2FirstNodeBlock.SetResult();
        await processTask;

        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121, N123 });
        N12.Messages.Should().BeEquivalentTo(new[] { Message.Error("N12 Error") });
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Error("N121 Error"), Message.Error("N121 Error") });
        N123.Messages.Should().BeEquivalentTo(new[] { Message.Error("N123 Error") });
    }

    private sealed class TestValidator : Validator<TestNode>, IEnumerable
    {
        private readonly Dictionary<TestNode, Func<string>> errorProducersByNode = new();

        public void Add(TestNode node, Func<string> errorProducer) => errorProducersByNode.Add(node, errorProducer);

        protected override IEnumerable<Message> ValidateNode(TestNode node)
        {
            if (errorProducersByNode.TryGetValue(node, out var errorProducer))
            {
                yield return Message.Error(errorProducer());
            }
        }

        public IEnumerator GetEnumerator() => throw new NotSupportedException();
    }
}