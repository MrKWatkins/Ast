using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class OrderedProcessorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestOrderedProcessor();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Process_OverrideTraversal()
    {
        var processor = new TestOrderedProcessor { TraversalOverride = BreadthFirstTraversal<TestNode>.Instance };
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.BreadthFirst(N1));
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessor { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessor { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestOrderedProcessor { ProcessNodeOverride = n => n == N13 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed()
    {
        var processor = new TestTypedProcessor();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1).OfType<BNode>());
    }

    [Test]
    public void Process_Typed_OverrideTraversal()
    {
        var processor = new TestTypedProcessor { TraversalOverride = BreadthFirstTraversal<TestNode>.Instance };
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.BreadthFirst(N1).OfType<BNode>());
    }

    [Test]
    public void Process_Typed_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ShouldProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ProcessNodeOverride = n => n == N122 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }
    
    public sealed class TestOrderedProcessor : OrderedProcessor<TestNode>
    {
        private readonly List<TestNode> processed = new();

        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
        public ITraversal<TestNode>? TraversalOverride { get; init; }

        public IEnumerable<TestNode> Processed => processed;

        protected override ITraversal<TestNode> Traversal => TraversalOverride ?? base.Traversal;

        protected override void ProcessNode(TestNode node)
        {
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(TestNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);

        protected override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);
    }

    private sealed class TestTypedProcessor : OrderedProcessor<TestNode, BNode>
    {
        private readonly List<TestNode> processed = new();

        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<BNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
        public ITraversal<TestNode>? TraversalOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected override ITraversal<TestNode> Traversal => TraversalOverride ?? base.Traversal;
        
        protected override void ProcessNode(BNode node)
        {
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(BNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);

        protected override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);
    }
}