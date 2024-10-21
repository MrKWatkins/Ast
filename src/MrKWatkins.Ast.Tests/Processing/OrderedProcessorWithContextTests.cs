using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class OrderedProcessorWithContextTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestOrderedProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(NodeCount);
    }

    [Test]
    public void Process_OverrideTraversal()
    {
        var processor = new TestOrderedProcessorWithContext { TraversalOverride = BreadthFirstTraversal<TestNode>.Instance };
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.BreadthFirst(N1));
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(NodeCount);
    }

    [Test]
    public void Process_CreateContextThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessorWithContext { CreateContextOverride = _ => throw exception };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during CreateContext.", N1)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessorWithContext { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessorWithContext { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessorWithContext { ProcessNodeOverride = n => n == N13 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed()
    {
        var processor = new TestTypedOrderedProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1).OfType<BNode>());
        processor.Context.Root.Should().BeSameAs(N1);
        processor.Context.NodesProcessed.Should().Be(BNodeCount);
    }

    [Test]
    public void Process_Typed_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedOrderedProcessorWithContext { ShouldProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedOrderedProcessorWithContext { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedOrderedProcessorWithContext { ProcessNodeOverride = n => n == N122 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    private sealed class TestOrderedProcessorWithContext : OrderedProcessorWithContext<TestContext, TestNode>
    {
        private readonly List<TestNode> processed = new();

        public TestContext Context { get; private set; } = null!;
        public Func<TestNode, TestContext>? CreateContextOverride { get; init; }
        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
        public ITraversal<TestNode>? TraversalOverride { get; init; }

        public IEnumerable<TestNode> Processed => processed;

        protected override ITraversal<TestNode> Traversal => TraversalOverride ?? base.Traversal;

        protected override TestContext CreateContext(TestNode root)
        {
            Context = CreateContextOverride?.Invoke(root) ?? new TestContext(root);
            return Context;
        }

        protected override void ProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            context.NodesProcessed++;
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(context, node);
        }

        protected override bool ShouldProcessChildren(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(context, node);
        }
    }

    private sealed class TestTypedOrderedProcessorWithContext : OrderedProcessorWithContext<TestContext, TestNode, BNode>
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

        protected override bool ShouldProcessChildren(TestContext context, TestNode node)
        {
            context.Should().BeSameAs(Context);
            return ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(context, node);
        }
    }

    private sealed class TestContext(TestNode root)
    {
        public TestNode Root { get; } = root;

        public int NodesProcessed { get; set; }
    }
}