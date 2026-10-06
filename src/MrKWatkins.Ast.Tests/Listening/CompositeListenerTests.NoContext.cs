using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed partial class CompositeListenerTests
{
    [Test]
    public void NoContext_With_ThrowsIfListenerForTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestNode>
            .Build()
            .With(new NoContextTestListener<ANode>());

        builder.Invoking(b => b.With(new NoContextTestListener<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A listener has already been registered for ANode.");
    }

    [Test]
    public void NoContext_With_ThrowsIfListenerForRootTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestNode>
            .Build()
            .With(new NoContextTestListener());

        builder.Invoking(b => b.With(new NoContextTestListener()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A listener has already been registered for TestNode.");
    }

    [Test]
    public void NoContext_ToListener_ThrowsIfNoListenersRegistered()
    {
        var builder = CompositeListener<TestNode>.Build();

        builder.Invoking(b => b.ToListener())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No listeners have been registered.");
    }

    [Test]
    public void NoContext_Listen_NoSubTypes()
    {
        var aListener = new NoContextTestListener<ANode>();
        var bChildListener = new NoContextTestListener<BChild>();
        var cListener = new NoContextTestListener<CNode>();

        var listener = CompositeListener<TestNode>
            .Build()
            .With(aListener)
            .With(bChildListener)
            .With(cListener)
            .ToListener();

        listener.Listen(N1);

        aListener.Count.Should().Equal(4);
        bChildListener.Count.Should().Equal(0);
        cListener.Count.Should().Equal(2);

        // Repeat to ensure cached handlers work.
        listener.Listen(N1);

        aListener.Count.Should().Equal(8);
        bChildListener.Count.Should().Equal(0);
        cListener.Count.Should().Equal(4);
    }

    [Test]
    public void NoContext_Listen_MostSpecificSubTypeHandlerUsed()
    {
        var bListener = new NoContextTestListener<BNode>();
        var bChildListener = new NoContextTestListener<BChild>();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(bListener)
            .With(bChildListener)
            .ToListener();

        listener.Listen(tree);

        bListener.Count.Should().Equal(1);
        bChildListener.Count.Should().Equal(2);
    }

    [Test]
    public void NoContext_Listen_RootType()
    {
        var rootListener = new NoContextTestListener();
        var bListener = new NoContextTestListener<BNode>();

        var tree = new ANode(new BNode(), new BChild(), new CNode());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(rootListener)
            .With(bListener)
            .ToListener();

        listener.Listen(tree);

        rootListener.Count.Should().Equal(2);
        bListener.Count.Should().Equal(2);
    }

    [Test]
    public void NoContext_Listen_ShouldListenToDescendants()
    {
        var doListenToChildren = new NoContextTestListener<ANode> { ListenToChildren = _ => true };
        var doNotListenToChildren = new NoContextTestListener<BNode> { ListenToChildren = _ => false };

        var tree = new ANode(new ANode(new CNode()), new BNode(new BChild()), new CNode());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(doListenToChildren)
            .With(doNotListenToChildren)
            .ToListener();

        listener.Listen(tree);

        doListenToChildren.Count.Should().Equal(2);
        doNotListenToChildren.Count.Should().Equal(1);
    }

    private sealed class NoContextTestListener : Listener<TestNode>
    {
        public int Count { get; private set; }

        protected internal override void ListenToNode(TestNode _) => Count++;
    }

    private sealed class NoContextTestListener<TNode> : NodeListener<TestNode, TNode>
        where TNode : TestNode
    {
        public Func<TNode, bool>? ListenToChildren { get; init; }

        public int Count { get; private set; }

        protected override void ListenToNode(TNode node) => Count++;

        protected override bool ShouldListenToDescendants(TNode node) => ListenToChildren?.Invoke(node) ?? base.ShouldListenToDescendants(node);
    }
}