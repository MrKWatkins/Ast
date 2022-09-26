using MrKWatkins.Ast.Enumeration;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ProcessorWithContextTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1));
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(NodeCount);
    }

    [Test]
    public void Process_OverrideEnumerator()
    {
        var processor = new TestProcessorWithContext { EnumeratorOverride = BreadthFirst<TestNode>.Instance };
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.BreadthFirst(N1));
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(NodeCount);
    }

    [Test]
    public void Process_CreateContextThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessorWithContext { CreateContextOverride = _ => throw exception };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException>()
            .WithMessage("Exception during CreateContext.")
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessorWithContext { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessorWithContext { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessorWithContext { ProcessNodeOverride = n => n == N13 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed()
    {
        var processor = new TestTypedProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1).OfType<BNode>());
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(BNodeCount);
    }

    [Test]
    public void Process_Typed_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessorWithContext { ShouldProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessorWithContext { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessorWithContext { ProcessNodeOverride = n => n == N122 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    private sealed class TestProcessorWithContext : ProcessorWithContext<TestContext, TestNode>
    {
        private readonly List<TestNode> processed = new();

        public TestContext Context { get; private set; } = null!;
        public Func<TestNode, TestContext>? CreateContextOverride { get; init; }
        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
        public IDescendentEnumerator<TestNode>? EnumeratorOverride { get; init; }

        public IEnumerable<TestNode> Processed => processed;

        protected internal override IDescendentEnumerator<TestNode> Enumerator => EnumeratorOverride ?? base.Enumerator;

        protected override TestContext CreateContext(TestNode root)
        {
            Context = CreateContextOverride?.Invoke(root) ?? new TestContext(root);
            return Context;
        }

        protected internal override void ProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            context.NodesProcessed++;
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected internal override bool ShouldProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(context, node);
        }

        protected internal override bool ShouldProcessChildren(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(context, node);
        }
    }
    
    private sealed class TestTypedProcessorWithContext : ProcessorWithContext<TestContext, TestNode, BNode>
    {
        private readonly List<TestNode> processed = new();

        public TestContext Context { get; private set; } = null!;
        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<BNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected override TestContext CreateContext(TestNode root) => Context = new TestContext(root);

        protected override void ProcessNode(TestContext context, BNode node)
        {
            context.Should().BeSameAs(Context);
            context.NodesProcessed++;
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(TestContext context, BNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(context, node);
        }

        protected internal override bool ShouldProcessChildren(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(context, node);
        }
    }

    private sealed class TestContext
    {
        public TestContext(TestNode root)
        {
            Root = root;
        }

        public TestNode Root { get; }
        
        public int NodesProcessed { get; set; }
    }
}