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
    public void ShouldProcessDescendents()
    {
        var processor = new TestOrderedNodeProcessor();

        processor.ShouldProcessDescendents(N11).Should().BeTrue();
        processor.ShouldProcessDescendentsCalled.Should().BeFalse();

        processor.ShouldProcessDescendents(N12).Should().BeTrue();
        processor.ShouldProcessDescendentsCalled.Should().BeTrue();
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
    public void WithContext_ShouldProcessDescendents()
    {
        var context = new object();
        var processor = new TestOrderedNodeProcessor<object>(context);

        processor.ShouldProcessDescendents(context, N11).Should().BeTrue();
        processor.ShouldProcessDescendentsCalled.Should().BeFalse();

        processor.ShouldProcessDescendents(context, N12).Should().BeTrue();
        processor.ShouldProcessDescendentsCalled.Should().BeTrue();
    }

    private sealed class TestOrderedNodeProcessor : OrderedNodeProcessor<TestNode, BNode>
    {
        public bool ProcessedCalled { get; private set; }
        public bool ShouldProcessDescendentsCalled { get; private set; }

        protected override void Process(BNode node) => ProcessedCalled = true;

        protected override bool ShouldProcessDescendents(BNode node)
        {
            ShouldProcessDescendentsCalled = true;
            return base.ShouldProcessDescendents(node);
        }
    }

    private sealed class TestOrderedNodeProcessor<TContext>(TContext expectedContext) : OrderedNodeProcessor<TContext, TestNode, BNode>
    {
        public bool ProcessedCalled { get; private set; }
        public bool ShouldProcessDescendentsCalled { get; private set; }

        protected override void Process(TContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            ProcessedCalled = true;
        }

        protected override bool ShouldProcessDescendents(TContext context, BNode node)
        {
            ShouldProcessDescendentsCalled = true;
            context.Should().BeTheSameInstanceAs(expectedContext);
            return base.ShouldProcessDescendents(context, node);
        }
    }
}