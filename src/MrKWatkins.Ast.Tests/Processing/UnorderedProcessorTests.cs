using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class UnorderedProcessorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestUnorderedProcessor();
        processor.Process(N1);
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestUnorderedProcessor { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ShouldProcessNode.", N13)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestUnorderedProcessor { ProcessNodeOverride = n => n == N13 ? throw exception : null };

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
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedProcessor { ProcessNodeOverride = n => n == N122 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>().Which.Should().Be(exception);
    }

    private sealed class TestTypedProcessor : UnorderedProcessor<TestNode, BNode>
    {
        private readonly List<TestNode> processed = new();

        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<BNode, bool>? ShouldProcessNodeOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected override void ProcessNode(BNode node)
        {
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(BNode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);
    }
}