using System.Collections;
using MrKWatkins.Ast.Enumeration;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelProcessorTests : TreeTestFixture
{
    [Test]
    public void Constructor_Params_ProcessorWithInvalidEnumeratorThrows() =>
        TestProcessorWithInvalidEnumeratorThrows(() => new ParallelProcessor<TestNode>(new TestValidator(), new BreadthFirstProcessor()));
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_Params_ProcessorWithInvalidEnumeratorThrows() =>
        TestProcessorWithInvalidEnumeratorThrows(() => new ParallelProcessor<TestNode>(5, new TestValidator(), new BreadthFirstProcessor()));
    
    [Test]
    public void Constructor_IEnumerable_ProcessorWithInvalidEnumeratorThrows() =>
        TestProcessorWithInvalidEnumeratorThrows(() => new ParallelProcessor<TestNode>(new List<Processor<TestNode>> { new TestValidator(), new BreadthFirstProcessor() }));
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_IEnumerable_ProcessorWithInvalidEnumeratorThrows() =>
        TestProcessorWithInvalidEnumeratorThrows(() => new ParallelProcessor<TestNode>(5, new List<Processor<TestNode>> { new TestValidator(), new BreadthFirstProcessor() }));
    
    private static void TestProcessorWithInvalidEnumeratorThrows(Func<ParallelProcessor<TestNode>> constructor) => 
        FluentActions.Invoking(constructor)
            .Should().Throw<ArgumentException>()
            .WithParameters("Value contains processor BreadthFirstProcessor that overrides Enumerator; only DepthFirstPreOrder processors can be used in parallel.", "processors");

    [Test]
    public void Constructor_Params_ProcessorsEmptyThrows() => TestProcessorsEmptyThrows(() => new ParallelProcessor<TestNode>());
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_Params_ProcessorsEmptyThrows() => TestProcessorsEmptyThrows(() => new ParallelProcessor<TestNode>(5));
    
    [Test]
    public void Constructor_IEnumerable_ProcessorsEmptyThrows() => TestProcessorsEmptyThrows(() => new ParallelProcessor<TestNode>(Enumerable.Empty<Processor<TestNode>>()));
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_IEnumerable_ProcessorsEmptyThrows() => TestProcessorsEmptyThrows(() => new ParallelProcessor<TestNode>(5, Enumerable.Empty<Processor<TestNode>>()));
    
    private static void TestProcessorsEmptyThrows(Func<ParallelProcessor<TestNode>> constructor) => 
        FluentActions.Invoking(constructor)
            .Should().Throw<ArgumentException>()
            .WithParameters("Value is empty.", "processors");

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_MaxDegreeOfParallelism_Params_InvalidMaxDegreeOfParallelismThrows(int maxDegreeOfParallelism) => 
        TestInvalidMaxDegreeOfParallelismThrows(maxDegreeOfParallelism, () => new ParallelProcessor<TestNode>(maxDegreeOfParallelism, new TestValidator()));

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_MaxDegreeOfParallelism_IEnumerable_InvalidMaxDegreeOfParallelismThrows(int maxDegreeOfParallelism) => 
        TestInvalidMaxDegreeOfParallelismThrows(maxDegreeOfParallelism, () => new ParallelProcessor<TestNode>(maxDegreeOfParallelism, new List<Processor<TestNode>> { new TestValidator() }));

    private static void TestInvalidMaxDegreeOfParallelismThrows(int maxDegreeOfParallelism, Func<ParallelProcessor<TestNode>> constructor) => 
        FluentActions.Invoking(constructor)
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithParameters("maxDegreeOfParallelism", maxDegreeOfParallelism, "Value must be greater than 0.");
    
    [Test]
    public void Constructor_Params() => 
        TestConstructor(Environment.ProcessorCount, () => new ParallelProcessor<TestNode>(new TestValidator(), new TestValidator()));
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_Params() => 
        TestConstructor(5, () => new ParallelProcessor<TestNode>(5, new TestValidator(), new TestValidator()));
    
    [Test]
    public void Constructor_IEnumerable() => 
        TestConstructor(Environment.ProcessorCount, () => new ParallelProcessor<TestNode>(new List<Processor<TestNode>> { new TestValidator(), new TestValidator() }));
    
    [Test]
    public void Constructor_MaxDegreeOfParallelism_IEnumerable() => 
        TestConstructor(5, () => new ParallelProcessor<TestNode>(5, new List<Processor<TestNode>> { new TestValidator(), new TestValidator() }));
    
    private static void TestConstructor(int expectedMaxDegreeOfParallelism, Func<ParallelProcessor<TestNode>> constructor)
    {
        var processor = constructor();
        processor.MaxDegreeOfParallelism.Should().Be(expectedMaxDegreeOfParallelism);
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
        
        var parallel = new ParallelProcessor<TestNode>(maxDegreeOfParallelism, validator1, validator2);
        
        parallel.Process(N1);
        
        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121, N123 });

        N12.Messages.Should().BeEquivalentTo(new [] { Message.Error("N12 Error") });
        
        // Note that without the lock in Node<TNode>.AddMessage this contains just one message a good percentage of the time.
        N121.Messages.Should().BeEquivalentTo(new [] { Message.Error("N121 Error"), Message.Error("N121 Error") });
        N123.Messages.Should().BeEquivalentTo(new [] { Message.Error("N123 Error") });
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
        
        var parallel = new ParallelProcessor<TestNode>(maxDegreeOfParallelism, validator1, validator2);

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

        N12.Messages.Should().BeEquivalentTo(new [] { Message.Error("N12 Error") });
        // Only one because the other threw.
        N121.Messages.Should().BeEquivalentTo(new [] { Message.Error("N121 Error") });
        N123.Messages.Should().BeEquivalentTo(new [] { Message.Error("N123 Error") });
    }
    
    [TestCase(1)]
    [TestCase(2)]
    public void Process_ShouldProcessChildrenReturnsFalse(int maxDegreeOfParallelism)
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
        validator2.ShouldProcessChildrenOverride = n => n != N121;
        
        var parallel = new ParallelProcessor<TestNode>(maxDegreeOfParallelism, validator1, validator2);

        parallel.Invoking(p => p.Process(N1)).Should().Throw<AggregateException>()
            .WithInnerException<ProcessingException<TestNode>>()
            .WithParameters("Processor TestValidator.ShouldProcessChildren returned false; child nodes cannot be skipped when running in parallel.", N121);

        // The other validations should still have been processed.
        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121, N123 });

        N12.Messages.Should().BeEquivalentTo(new [] { Message.Error("N12 Error") });
        // Still has two because we check ShouldProcessChildren after the validation.
        N121.Messages.Should().BeEquivalentTo(new [] { Message.Error("N121 Error"), Message.Error("N121 Error") });  
        N123.Messages.Should().BeEquivalentTo(new [] { Message.Error("N123 Error") });
    }
    
    [Test]
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
        
        var parallel = new ParallelProcessor<TestNode>(3, validator1, validator2);

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
        N12.Messages.Should().BeEquivalentTo(new [] { Message.Error("N12 Error") });
        N121.Messages.Should().BeEquivalentTo(new [] { Message.Error("N121 Error"), Message.Error("N121 Error") });  
        N123.Messages.Should().BeEquivalentTo(new [] { Message.Error("N123 Error") });
    }
    
    private sealed class TestValidator : Validator<TestNode>, IEnumerable
    {
        private readonly Dictionary<TestNode, Func<string>> errorProducersByNode = new();
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; set; }

        public void Add(TestNode node, Func<string> errorProducer) => errorProducersByNode.Add(node, errorProducer);
        
        protected override IEnumerable<Message> ValidateNode(TestNode node)
        {
            if (errorProducersByNode.TryGetValue(node, out var errorProducer))
            {
                yield return Message.Error(errorProducer());
            }
        }

        protected internal override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);

        public IEnumerator GetEnumerator() => throw new NotSupportedException();
    }

    private sealed class BreadthFirstProcessor : Processor<TestNode>
    {
        protected internal override IDescendentEnumerator<TestNode> Enumerator => BreadthFirst<TestNode>.Instance;

        protected internal override void ProcessNode(TestNode node) => throw new NotSupportedException();
    }
}