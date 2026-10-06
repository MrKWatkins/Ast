using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Tests.Visiting;

public sealed partial class VisitorTests : TreeTestFixture
{
    [Test]
    public void Visit()
    {
        var visitor = new TestVisitor();

        var context = new TestContext();

        var result = visitor.Visit(context, N1);

        result.Should().Equal("(N1(N11(N111))(N12(N121)(N122)(N123))(N13))");
        context.Count.Should().Equal(NodeCount);
    }

    [Test]
    public void VisitChildren_ReturnsResultsInOrder()
    {
        var visitor = new NameVisitor();

        var results = visitor.Children(new TestContext(), N12);

        results.Should().SequenceEqual("N121", "N122", "N123");
    }

    [Test]
    public void VisitChildren_NoChildren()
    {
        var visitor = new NameVisitor();

        var results = visitor.Children(new TestContext(), N111);

        results.Should().BeEmpty();
    }

    [Test]
    public void Visit_Typed()
    {
        var visitor = new TypedTestVisitor<BNode>();

        var context = new TestContext();

        var result = visitor.Visit(context, N12);

        result.Should().Equal("N12");
        context.Count.Should().Equal(1);
    }

    [Test]
    public void Visit_Typed_UnhandledNodeThrowsByDefault()
    {
        var visitor = new TypedTestVisitor<BNode>();

        var context = new TestContext();

        visitor.Invoking(v => v.Visit(context, N1))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("TypedTestVisitor<BNode> cannot visit nodes of type ANode.");

        context.Count.Should().Equal(0);
    }

    [Test]
    public void Visit_Typed_UnhandledNodeCanBeOverridden()
    {
        var visitor = new TypedTestVisitor<BNode> { Unhandled = (_, node) => $"Unhandled {node.Name}" };

        var context = new TestContext();

        var result = visitor.Visit(context, N1);

        result.Should().Equal("Unhandled N1");
        context.Count.Should().Equal(0);
    }

    [Test]
    public void Visit_Typed_ChildrenOfOtherTypesThrowWhenNotInAComposite()
    {
        var visitor = new TypedTestVisitor<ANode> { IncludeChildren = true };

        // N1 is an ANode, as is its first child N11, but its second child N12 is a BNode.
        visitor.Invoking(v => v.Visit(new TestContext(), N1))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("TypedTestVisitor<ANode> cannot visit nodes of type BNode.");
    }

    private sealed class TestVisitor : Visitor<TestContext, TestNode, string>
    {
        protected internal override string VisitNode(TestContext context, TestNode node)
        {
            context.Count++;
            return $"({node.Name}{string.Concat(VisitChildren(context, node))})";
        }
    }

    private sealed class NameVisitor : Visitor<TestContext, TestNode, string>
    {
        public string[] Children(TestContext context, TestNode node) => VisitChildren(context, node);

        protected internal override string VisitNode(TestContext context, TestNode node) => node.Name;
    }

    private sealed class TypedTestVisitor<TNode> : NodeVisitor<TestContext, TestNode, TNode, string>
        where TNode : TestNode
    {
        public Func<TestContext, TestNode, string>? Unhandled { get; init; }

        public bool IncludeChildren { get; init; }

        protected override string VisitNode(TestContext context, TNode node)
        {
            context.Count++;
            return IncludeChildren ? $"{node.Name}{string.Concat(VisitChildren(context, node))}" : node.Name;
        }

        protected override string VisitUnhandledNode(TestContext context, TestNode node) =>
            Unhandled?.Invoke(context, node) ?? base.VisitUnhandledNode(context, node);
    }

    private sealed class TestContext
    {
        public int Count { get; set; }
    }
}