using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class OrderedNodeProcessorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var processor = new TestOrderedNodeProcessor();

        processor.Process(N11);
        processor.ProcessedCalled.Should().BeFalse();

        processor.Process(N12);
        processor.ProcessedCalled.Should().BeTrue();
    }

    [Test]
    public void ShouldProcessDescendants()
    {
        var processor = new TestOrderedNodeProcessor();

        processor.ShouldProcessDescendants(N11).Should().BeTrue();
        processor.ShouldProcessDescendantsCalled.Should().BeFalse();

        processor.ShouldProcessDescendants(N12).Should().BeTrue();
        processor.ShouldProcessDescendantsCalled.Should().BeTrue();
    }

    [Test]
    public void WithContext_Process()
    {
        var context = new object();
        var processor = new TestOrderedNodeProcessor<object>(context);

        processor.Process(context, N11);
        processor.ProcessedCalled.Should().BeFalse();

        processor.Process(context, N12);
        processor.ProcessedCalled.Should().BeTrue();
    }

    [Test]
    public void WithContext_ShouldProcessDescendants()
    {
        var context = new object();
        var processor = new TestOrderedNodeProcessor<object>(context);

        processor.ShouldProcessDescendants(context, N11).Should().BeTrue();
        processor.ShouldProcessDescendantsCalled.Should().BeFalse();

        processor.ShouldProcessDescendants(context, N12).Should().BeTrue();
        processor.ShouldProcessDescendantsCalled.Should().BeTrue();
    }

    private sealed class TestOrderedNodeProcessor : OrderedNodeProcessor<TestNode, BNode>
    {
        public bool ProcessedCalled { get; private set; }
        public bool ShouldProcessDescendantsCalled { get; private set; }

        protected override void Process(BNode node) => ProcessedCalled = true;

        protected override bool ShouldProcessDescendants(BNode node)
        {
            ShouldProcessDescendantsCalled = true;
            return base.ShouldProcessDescendants(node);
        }
    }

    private sealed class TestOrderedNodeProcessor<TContext>(TContext expectedContext) : OrderedNodeProcessor<TContext, TestNode, BNode>
    {
        public bool ProcessedCalled { get; private set; }
        public bool ShouldProcessDescendantsCalled { get; private set; }

        protected override void Process(TContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            ProcessedCalled = true;
        }

        protected override bool ShouldProcessDescendants(TContext context, BNode node)
        {
            ShouldProcessDescendantsCalled = true;
            context.Should().BeTheSameInstanceAs(expectedContext);
            return base.ShouldProcessDescendants(context, node);
        }
    }
}