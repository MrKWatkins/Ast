using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Tests.Visiting;

public sealed partial class VisitorTests
{
    [Test]
    public void NoContext_Visit()
    {
        var visitor = new NoContextTestVisitor();

        var result = visitor.Visit(N1);

        result.Should().Equal("(N1(N11(N111))(N12(N121)(N122)(N123))(N13))");
        visitor.Count.Should().Equal(NodeCount);
    }

    [Test]
    public void NoContext_VisitChildren_ReturnsResultsInOrder()
    {
        var visitor = new NoContextNameVisitor();

        var results = visitor.Children(N12);

        results.Should().SequenceEqual("N121", "N122", "N123");
    }

    [Test]
    public void NoContext_VisitChildren_NoChildren()
    {
        var visitor = new NoContextNameVisitor();

        var results = visitor.Children(N111);

        results.Should().BeEmpty();
    }

    [Test]
    public void NoContext_Visit_Typed()
    {
        var visitor = new NoContextTypedTestVisitor<BNode>();

        var result = visitor.Visit(N12);

        result.Should().Equal("N12");
        visitor.Count.Should().Equal(1);
    }

    [Test]
    public void NoContext_Visit_Typed_UnhandledNodeThrowsByDefault()
    {
        var visitor = new NoContextTypedTestVisitor<BNode>();

        visitor.Invoking(v => v.Visit(N1))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("NoContextTypedTestVisitor<BNode> cannot visit nodes of type ANode.");

        visitor.Count.Should().Equal(0);
    }

    [Test]
    public void NoContext_Visit_Typed_UnhandledNodeCanBeOverridden()
    {
        var visitor = new NoContextTypedTestVisitor<BNode> { Unhandled = node => $"Unhandled {node.Name}" };

        var result = visitor.Visit(N1);

        result.Should().Equal("Unhandled N1");
        visitor.Count.Should().Equal(0);
    }

    [Test]
    public void NoContext_Visit_Typed_ChildrenOfOtherTypesThrowWhenNotInAComposite()
    {
        var visitor = new NoContextTypedTestVisitor<ANode> { IncludeChildren = true };

        visitor.Invoking(v => v.Visit(N1))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("NoContextTypedTestVisitor<ANode> cannot visit nodes of type BNode.");
    }

    private sealed class NoContextTestVisitor : Visitor<TestNode, string>
    {
        public int Count { get; private set; }

        protected internal override string VisitNode(TestNode node)
        {
            Count++;
            return $"({node.Name}{string.Concat(VisitChildren(node))})";
        }
    }

    private sealed class NoContextNameVisitor : Visitor<TestNode, string>
    {
        public string[] Children(TestNode node) => VisitChildren(node);

        protected internal override string VisitNode(TestNode node) => node.Name;
    }

    private sealed class NoContextTypedTestVisitor<TNode> : NodeVisitor<TestNode, TNode, string>
        where TNode : TestNode
    {
        public Func<TestNode, string>? Unhandled { get; init; }

        public bool IncludeChildren { get; init; }

        public int Count { get; private set; }

        protected override string VisitNode(TNode node)
        {
            Count++;
            return IncludeChildren ? $"{node.Name}{string.Concat(VisitChildren(node))}" : node.Name;
        }

        protected override string VisitUnhandledNode(TestNode node) => Unhandled?.Invoke(node) ?? base.VisitUnhandledNode(node);
    }
}