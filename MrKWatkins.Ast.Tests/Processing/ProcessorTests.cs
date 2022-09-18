using MrKWatkins.Ast.Enumeration;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ProcessorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestProcessor();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1));
    }
    
    [Test]
    public void Process_OverrideEnumerator()
    {
        var processor = new TestProcessor { EnumeratorOverride = BreadthFirst<TestNode>.Instance };
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.BreadthFirst(N1));
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessor { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", exception, N13);
    }

    [Test]
    public void Process_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessor { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", exception, N13);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestProcessor { ProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", exception, N13);
    }
    
    [Test]
    public void Process_Typed()
    {
        var processor = new TestTypedProcessor();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1).OfType<BNode>());
    }

    [Test]
    public void Process_Typed_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ShouldProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", exception, N122);
    }

    [Test]
    public void Process_Typed_ShouldProcessChildrenThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ShouldProcessChildrenOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessChildren.", exception, N13);
    }

    [Test]
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", exception, N122);
    }
    
    private sealed class TestProcessor : Processor<TestNode>
    {
        private readonly List<TestNode> processed = new();
        
        public Func<TestNode, bool>? ProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }
        public IDescendentEnumerator<TestNode>? EnumeratorOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected internal override IDescendentEnumerator<TestNode> Enumerator => EnumeratorOverride ?? base.Enumerator;

        protected internal override void ProcessNode(TestNode node)
        {
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected internal override bool ShouldProcessNode(TestNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);
        
        protected internal override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);
    }
    
    private sealed class TestTypedProcessor : Processor<BNode, TestNode>
    {
        private readonly List<TestNode> processed = new();
        
        public Func<BNode, bool>? ProcessNodeOverride { get; init; }
        public Func<BNode, bool>? ShouldProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessChildrenOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected override void ProcessNode(BNode node)
        {
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(BNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);
        
        protected internal override bool ShouldProcessChildren(TestNode node) => ShouldProcessChildrenOverride?.Invoke(node) ?? base.ShouldProcessChildren(node);
    }
}