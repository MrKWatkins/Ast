using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Tests.Visiting;

public sealed partial class CompositeVisitorTests
{
    [Test]
    public void NoContext_With_ThrowsIfVisitorForTypeAlreadyRegistered()
    {
        var builder = CompositeVisitor<TestNode, string>
            .Build()
            .With(new NoContextTestVisitor<ANode>());

        builder.Invoking(b => b.With(new NoContextTestVisitor<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A visitor has already been registered for ANode.");
    }

    [Test]
    public void NoContext_With_ThrowsIfVisitorForRootTypeAlreadyRegistered()
    {
        var builder = CompositeVisitor<TestNode, string>
            .Build()
            .With(new NoContextTestVisitor());

        builder.Invoking(b => b.With(new NoContextTestVisitor()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A visitor has already been registered for TestNode.");
    }

    [Test]
    public void NoContext_With_ThrowsIfVisitorAlreadyRegisteredWithAnotherComposite()
    {
        var visitor = new NoContextTestVisitor<ANode>();

        _ = CompositeVisitor<TestNode, string>
            .Build()
            .With(visitor);

        var builder = CompositeVisitor<TestNode, string>.Build();

        builder.Invoking(b => b.With(visitor))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("The visitor NoContextTestVisitor<ANode> has already been registered with a composite visitor.");
    }

    [Test]
    public void NoContext_ToVisitor_ThrowsIfNoVisitorsRegistered()
    {
        var builder = CompositeVisitor<TestNode, string>.Build();

        builder.Invoking(b => b.ToVisitor())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No visitors have been registered.");
    }

    [Test]
    public void NoContext_Visit_ThrowsIfNoVisitorRegisteredForType()
    {
        var visitor = CompositeVisitor<TestNode, string>
            .Build()
            .With(new NoContextTestVisitor<ANode>())
            .ToVisitor();

        visitor.Invoking(v => v.Visit(N12))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No visitor has been registered for BNode.");
    }

    [Test]
    public void NoContext_Visit_ChildrenAreDispatchedThroughTheComposite()
    {
        var aVisitor = new NoContextTestVisitor<ANode>();
        var bVisitor = new NoContextTestVisitor<BNode>();
        var cVisitor = new NoContextTestVisitor<CNode>();

        var visitor = CompositeVisitor<TestNode, string>
            .Build()
            .With(aVisitor)
            .With(bVisitor)
            .With(cVisitor)
            .ToVisitor();

        var result = visitor.Visit(N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
        aVisitor.Count.Should().Equal(4);
        bVisitor.Count.Should().Equal(2);
        cVisitor.Count.Should().Equal(2);

        // Repeat to ensure cached visitors work.
        result = visitor.Visit(N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
        aVisitor.Count.Should().Equal(8);
        bVisitor.Count.Should().Equal(4);
        cVisitor.Count.Should().Equal(4);
    }

    [Test]
    public void NoContext_Visit_RegisteredVisitorDispatchesThroughTheComposite()
    {
        var aVisitor = new NoContextTestVisitor<ANode>();
        var bVisitor = new NoContextTestVisitor<BNode>();

        _ = CompositeVisitor<TestNode, string>
            .Build()
            .With(aVisitor)
            .With(bVisitor)
            .ToVisitor();

        var tree = new ANode(new BNode(), new ANode());

        var result = bVisitor.Visit(tree);

        result.Should().Equal("A:ANode(B:BNodeA:ANode)");
        aVisitor.Count.Should().Equal(2);
        bVisitor.Count.Should().Equal(1);
    }

    [Test]
    public void NoContext_Visit_MostSpecificSubTypeVisitorUsed()
    {
        var bVisitor = new NoContextTestVisitor<BNode>();
        var bChildVisitor = new NoContextTestVisitor<BChild>("BC");

        var tree = new BNode(new BChild(), new BGrandChild());

        var visitor = CompositeVisitor<TestNode, string>
            .Build()
            .With(bVisitor)
            .With(bChildVisitor)
            .ToVisitor();

        var result = visitor.Visit(tree);

        result.Should().Equal("B:BNode(BC:BChildBC:BGrandChild)");
        bVisitor.Count.Should().Equal(1);
        bChildVisitor.Count.Should().Equal(2);
    }

    [Test]
    public void NoContext_Visit_RootTypeFallback()
    {
        var rootVisitor = new NoContextTestVisitor();
        var bVisitor = new NoContextTestVisitor<BNode>();

        var visitor = CompositeVisitor<TestNode, string>
            .Build()
            .With(rootVisitor)
            .With(bVisitor)
            .ToVisitor();

        var result = visitor.Visit(N1);

        result.Should().Equal("*:N1(*:N11(*:N111)B:N12(*:N121B:N122*:N123)*:N13)");
        rootVisitor.Count.Should().Equal(6);
        bVisitor.Count.Should().Equal(2);
    }

    [Test]
    public void NoContext_Visit_NestedComposite_DispatchesThroughTheOutermostComposite()
    {
        var aVisitor = new NoContextTestVisitor<ANode>();
        var bVisitor = new NoContextTestVisitor<BNode>();
        var cVisitor = new NoContextTestVisitor<CNode>();

        var inner = CompositeVisitor<TestNode, string>
            .Build()
            .With(bVisitor)
            .With(cVisitor)
            .ToVisitor();

        var outer = CompositeVisitor<TestNode, string>
            .Build()
            .With(aVisitor)
            .With(inner)
            .ToVisitor();

        var result = outer.Visit(N1);

        result.Should().Equal("A:N1(A:N11(A:N111)B:N12(A:N121B:N122C:N123)C:N13)");
    }

    private sealed class NoContextTestVisitor : Visitor<TestNode, string>
    {
        public int Count { get; private set; }

        protected internal override string VisitNode(TestNode node)
        {
            Count++;
            return Format("*", node, VisitChildren(node));
        }
    }

    private sealed class NoContextTestVisitor<TNode> : NodeVisitor<TestNode, TNode, string>
        where TNode : TestNode
    {
        private readonly string prefix;

        public NoContextTestVisitor(string? prefix = null)
        {
            this.prefix = prefix ?? typeof(TNode).Name[..1];
        }

        public int Count { get; private set; }

        protected override string VisitNode(TNode node)
        {
            Count++;
            return Format(prefix, node, VisitChildren(node));
        }
    }
}