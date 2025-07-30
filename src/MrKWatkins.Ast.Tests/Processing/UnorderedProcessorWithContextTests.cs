using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class UnorderedProcessorWithContextTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestUnorderedProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1));
        processor.Context.Root.Should().BeTheSameInstanceAs(N1);
        processor.Context.NodesProcessed.Should().Equal(NodeCount);
    }

    [Test]
    public void Process_CreateContextThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestUnorderedProcessorWithContext { CreateContextOverride = _ => throw exception };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during CreateContext.", N1).And
            .HaveInnerException(exception);
    }

    [Test]
    public void Process_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestUnorderedProcessorWithContext { ShouldProcessNodeOverride = n => n == N13 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during ShouldProcessNode.", N13).And
            .HaveInnerException(exception);
    }

    [Test]
    public void Process_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestUnorderedProcessorWithContext { ProcessNodeOverride = n => n == N13 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during ProcessNode.", N13).And
            .HaveInnerException(exception);
    }

    [Test]
    public void Process_Typed()
    {
        var processor = new TestTypedUnorderedProcessorWithContext();
        processor.Process(N1);
        processor.Processed.Should().SequenceEqual(TestNode.Traverse.DepthFirstPreOrder(N1).OfType<BNode>());
        processor.Context.Root.Should().BeTheSameInstanceAs(N1);
        processor.Context.NodesProcessed.Should().Equal(BNodeCount);
    }

    [Test]
    public void Process_Typed_ShouldProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedUnorderedProcessorWithContext { ShouldProcessNodeOverride = n => n == N122 ? throw exception : true };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during ShouldProcessNode.", N122).And
            .HaveInnerException(exception);
    }

    [Test]
    public void Process_Typed_ProcessNodeThrows()
    {
        var exception = new InvalidOperationException("Test");
        var processor = new TestTypedUnorderedProcessorWithContext { ProcessNodeOverride = n => n == N122 ? throw exception : null };

        processor.Invoking(p => p.Process(N1))
            .Should().Throw<AggregateException>().That.Should()
            .HaveInnerException<ProcessingException<TestNode>>().That.Should()
            .HaveParameters("Exception during ProcessNode.", N122).And
            .HaveInnerException(exception);
    }

    private sealed class TestUnorderedProcessorWithContext : UnorderedProcessorWithContext<TestContext, TestNode>
    {
        private readonly List<TestNode> processed = new();

        public TestContext Context { get; private set; } = null!;
        public Func<TestNode, TestContext>? CreateContextOverride { get; init; }
        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; init; }

        public IEnumerable<TestNode> Processed => processed;

        protected override TestContext CreateContext(TestNode root)
        {
            Context = CreateContextOverride?.Invoke(root) ?? new TestContext(root);
            return Context;
        }

        protected override void ProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeTheSameInstanceAs(Context);
            context.NodesProcessed++;
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(TestContext context, TestNode node)
        {
            context.Should().BeTheSameInstanceAs(Context);
            return ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(context, node);
        }
    }

    private sealed class TestTypedUnorderedProcessorWithContext : UnorderedProcessorWithContext<TestContext, TestNode, BNode>
    {
        private readonly List<TestNode> processed = new();

        public TestContext Context { get; private set; } = null!;
        public Func<TestNode, object?>? ProcessNodeOverride { get; init; }
        public Func<BNode, bool>? ShouldProcessNodeOverride { get; init; }

        public IReadOnlyList<TestNode> Processed => processed;

        protected override TestContext CreateContext(TestNode root) => Context = new TestContext(root);

        protected override void ProcessNode(TestContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(Context);
            context.NodesProcessed++;
            processed.Add(node);
            ProcessNodeOverride?.Invoke(node);
        }

        protected override bool ShouldProcessNode(TestContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(Context);
            return ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(context, node);
        }
    }

    private sealed class TestContext(TestNode root)
    {
        public TestNode Root { get; } = root;

    public int NodesProcessed { get; set; }
}
}