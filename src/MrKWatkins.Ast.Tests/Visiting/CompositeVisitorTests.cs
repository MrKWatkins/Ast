using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Tests.Visiting;

public sealed partial class CompositeVisitorTests : TreeTestFixture
{
    [Test]
    public void With_ThrowsIfVisitorForTypeAlreadyRegistered()
    {
        var builder = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(new TestVisitor<ANode>());

        builder.Invoking(b => b.With(new TestVisitor<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A visitor has already been registered for ANode.");
    }

    [Test]
    public void With_ThrowsIfVisitorForRootTypeAlreadyRegistered()
    {
        var builder = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(new TestVisitor());

        builder.Invoking(b => b.With(new TestVisitor()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A visitor has already been registered for TestNode.");
    }

    [Test]
    public void With_ThrowsIfVisitorAlreadyRegisteredWithAnotherComposite()
    {
        var visitor = new TestVisitor<ANode>();

        _ = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(visitor);

        var builder = CompositeVisitor<TestContext, TestNode, string>.Build();

        builder.Invoking(b => b.With(visitor))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("The visitor TestVisitor<ANode> has already been registered with a composite visitor.");
    }

    [Test]
    public void ToVisitor_ThrowsIfNoVisitorsRegistered()
    {
        var builder = CompositeVisitor<TestContext, TestNode, string>.Build();

        builder.Invoking(b => b.ToVisitor())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No visitors have been registered.");
    }

    [Test]
    public void Visit_ThrowsIfNoVisitorRegisteredForType()
    {
        var visitor = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(new TestVisitor<ANode>())
            .ToVisitor();

        var context = new TestContext();

        visitor.Invoking(v => v.Visit(context, N12))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No visitor has been registered for BNode.");
    }

    [Test]
    public void Visit_ChildrenAreDispatchedThroughTheComposite()
    {
        var aVisitor = new TestVisitor<ANode>();
        var bVisitor = new TestVisitor<BNode>();
        var cVisitor = new TestVisitor<CNode>();

        var visitor = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(aVisitor)
            .With(bVisitor)
            .With(cVisitor)
            .ToVisitor();

        var context = new TestContext();

        var result = visitor.Visit(context, N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
        context.Count.Should().Equal(NodeCount);
        aVisitor.Count.Should().Equal(4);
        bVisitor.Count.Should().Equal(2);
        cVisitor.Count.Should().Equal(2);

        // Repeat to ensure cached visitors work.
        result = visitor.Visit(context, N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
        context.Count.Should().Equal(NodeCount * 2);
        aVisitor.Count.Should().Equal(8);
        bVisitor.Count.Should().Equal(4);
        cVisitor.Count.Should().Equal(4);
    }

    [Test]
    public void Visit_RegisteredVisitorDispatchesThroughTheComposite()
    {
        var aVisitor = new TestVisitor<ANode>();
        var bVisitor = new TestVisitor<BNode>();

        _ = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(aVisitor)
            .With(bVisitor)
            .ToVisitor();

        var tree = new ANode(new BNode(), new ANode());

        // Calling Visit on a registered visitor directly still goes through the composite, even for the root node.
        var result = bVisitor.Visit(new TestContext(), tree);

        result.Should().Equal("A:ANode(B:BNodeA:ANode)");
        aVisitor.Count.Should().Equal(2);
        bVisitor.Count.Should().Equal(1);
    }

    [Test]
    public void Visit_SubType()
    {
        var bVisitor = new TestVisitor<BNode>();

        var tree = new BNode(new BChild(), new BGrandChild());

        var visitor = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(bVisitor)
            .ToVisitor();

        var context = new TestContext();

        var result = visitor.Visit(context, tree);

        result.Should().Equal("B:BNode(B:BChildB:BGrandChild)");
        bVisitor.Count.Should().Equal(3);

        // Repeat to ensure cached visitors work.
        result = visitor.Visit(context, tree);

        result.Should().Equal("B:BNode(B:BChildB:BGrandChild)");
        bVisitor.Count.Should().Equal(6);
    }

    [Test]
    public void Visit_MostSpecificSubTypeVisitorUsed()
    {
        var bVisitor = new TestVisitor<BNode>();
        var bChildVisitor = new TestVisitor<BChild>("BC");

        var tree = new BNode(new BChild(), new BGrandChild());

        var visitor = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(bVisitor)
            .With(bChildVisitor)
            .ToVisitor();

        var context = new TestContext();

        var result = visitor.Visit(context, tree);

        result.Should().Equal("B:BNode(BC:BChildBC:BGrandChild)");
        bVisitor.Count.Should().Equal(1);
        bChildVisitor.Count.Should().Equal(2);

        // Repeat to ensure cached visitors work.
        result = visitor.Visit(context, tree);

        result.Should().Equal("B:BNode(BC:BChildBC:BGrandChild)");
        bVisitor.Count.Should().Equal(2);
        bChildVisitor.Count.Should().Equal(4);
    }

    [Test]
    public void Visit_RootTypeFallback()
    {
        var rootVisitor = new TestVisitor();
        var bVisitor = new TestVisitor<BNode>();

        var visitor = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(rootVisitor)
            .With(bVisitor)
            .ToVisitor();

        var context = new TestContext();

        var result = visitor.Visit(context, N1);

        result.Should().Equal("*:N1(*:N11(*:N111)B:N12(*:N121B:N122*:N123)*:N13)");
        rootVisitor.Count.Should().Equal(6);
        bVisitor.Count.Should().Equal(2);

        // Repeat to ensure cached visitors work.
        result = visitor.Visit(context, N1);

        result.Should().Equal("*:N1(*:N11(*:N111)B:N12(*:N121B:N122*:N123)*:N13)");
        rootVisitor.Count.Should().Equal(12);
        bVisitor.Count.Should().Equal(4);
    }

    [Test]
    public void Visit_NestedComposite_DispatchesThroughTheOutermostComposite()
    {
        var aVisitor = new TestVisitor<ANode>();
        var bVisitor = new TestVisitor<BNode>();
        var cVisitor = new TestVisitor<CNode>();

        var inner = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(bVisitor)
            .With(cVisitor)
            .ToVisitor();

        var outer = CompositeVisitor<TestContext, TestNode, string>
            .Build()
            .With(aVisitor)
            .With(inner)
            .ToVisitor();

        var context = new TestContext();

        // N12 is a BNode handled by the inner composite; its child N121 is an ANode which only the outer composite can handle.
        var result = outer.Visit(context, N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
        context.Count.Should().Equal(NodeCount);
    }

    private class BChild : BNode;

    private sealed class BGrandChild : BChild;

    private sealed class TestVisitor : Visitor<TestContext, TestNode, string>
    {
        public int Count { get; private set; }

        protected internal override string VisitNode(TestContext context, TestNode node)
        {
            context.Count++;
            Count++;
            return Format("*", node, VisitChildren(context, node));
        }
    }

    private sealed class TestVisitor<TNode> : NodeVisitor<TestContext, TestNode, TNode, string>
        where TNode : TestNode
    {
        private readonly string prefix;

        public TestVisitor(string? prefix = null)
        {
            this.prefix = prefix ?? typeof(TNode).Name[..1];
        }

        public int Count { get; private set; }

        protected override string VisitNode(TestContext context, TNode node)
        {
            context.Count++;
            Count++;
            return Format(prefix, node, VisitChildren(context, node));
        }
    }

    private static string Format(string prefix, TestNode node, string[] children)
    {
        var name = node.ToString();
        return children.Length == 0 ? $"{prefix}:{name}" : $"{prefix}:{name}({string.Concat(children)})";
    }

    private sealed class TestContext
    {
        public int Count { get; set; }
    }
}