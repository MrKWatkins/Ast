using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class CompositeListenerTests : TreeTestFixture
{
    [Test]
    public void With_ThrowsIfListenerForTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestContext, TestNode>
            .Build()
            .With(new TestListener<ANode>());

        builder.Invoking(b => b.With(new TestListener<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A listener has already been registered for ANode.");
    }

    [Test]
    public void With_ThrowsIfListenerForRootTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestContext, TestNode>
            .Build()
            .With(new TestListener());

        builder.Invoking(b => b.With(new TestListener()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A listener has already been registered for TestNode.");
    }

    [Test]
    public void ToListener_ThrowsIfNoListenersRegistered()
    {
        var builder = CompositeListener<TestContext, TestNode>.Build();

        builder.Invoking(b => b.ToListener())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("No listeners have been registered.");
    }

    [Test]
    public void Listen_NoSubTypes()
    {
        var aListener = new TestListener<ANode>();
        var bChildListener = new TestListener<BChild>();
        var cListener = new TestListener<CNode>();

        var listener = CompositeListener<TestContext, TestNode>
            .Build()
            .With(aListener)
            .With(bChildListener)
            .With(cListener)
            .ToListener();

        var context = new TestContext();

        listener.Listen(context, N1);

        context.Count.Should().Equal(6);
        aListener.Count.Should().Equal(4);
        bChildListener.Count.Should().Equal(0);
        cListener.Count.Should().Equal(2);

        // Repeat to ensure cached handlers work.
        listener.Listen(context, N1);

        context.Count.Should().Equal(12);
        aListener.Count.Should().Equal(8);
        bChildListener.Count.Should().Equal(0);
        cListener.Count.Should().Equal(4);
    }

    [Test]
    public void Listen_SubType()
    {
        var bListener = new TestListener<BNode>();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListener<TestContext, TestNode>
            .Build()
            .With(bListener)
            .ToListener();

        var context = new TestContext();

        listener.Listen(context, tree);

        context.Count.Should().Equal(3);
        bListener.Count.Should().Equal(3);

        // Repeat to ensure cached handlers work.
        context = new TestContext();
        listener.Listen(context, tree);

        context.Count.Should().Equal(3);
        bListener.Count.Should().Equal(6);
    }

    [Test]
    public void Listen_MostSpecificSubTypeHandlerUser()
    {
        var bListener = new TestListener<BNode>();
        var bChildListener = new TestListener<BChild>();

        var tree = new ANode(new BChild(), new BGrandChild());

        var listener = CompositeListener<TestContext, TestNode>
            .Build()
            .With(bListener)
            .With(bChildListener)
            .ToListener();

        var context = new TestContext();

        listener.Listen(context, tree);

        context.Count.Should().Equal(2);
        bListener.Count.Should().Equal(0);
        bChildListener.Count.Should().Equal(2);

        // Repeat to ensure cached handlers work.
        listener.Listen(context, tree);

        context.Count.Should().Equal(4);
        bListener.Count.Should().Equal(0);
        bChildListener.Count.Should().Equal(4);
    }

    [Test]
    public void Listen_RootType()
    {
        var rootListener = new TestListener();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListener<TestContext, TestNode>
            .Build()
            .With(rootListener)
            .ToListener();

        var context = new TestContext();
        listener.Listen(context, tree);

        context.Count.Should().Equal(5);
        rootListener.Count.Should().Equal(5);

        // Repeat to ensure cached handlers work.
        listener.Listen(context, tree);

        context.Count.Should().Equal(10);
        rootListener.Count.Should().Equal(10);
    }

    [Test]
    public void Listen_ShouldListenToChildren()
    {
        var doListenToChildren = new TestListener<ANode> { ListenToChildren = (_, _) => true };
        var doNotListenToChildren = new TestListener<BNode> { ListenToChildren = (_, _) => false };

        var tree = new ANode(new ANode(new CNode()), new BNode(new BChild()), new CNode());

        var listener = CompositeListener<TestContext, TestNode>
            .Build()
            .With(doListenToChildren)
            .With(doNotListenToChildren)
            .ToListener();

        var context = new TestContext();
        listener.Listen(context, tree);

        context.Count.Should().Equal(3);
        doListenToChildren.Count.Should().Equal(2);
        doNotListenToChildren.Count.Should().Equal(1);

        // Repeat to ensure cached handlers work.
        listener.Listen(context, tree);

        context.Count.Should().Equal(6);
        doListenToChildren.Count.Should().Equal(4);
        doNotListenToChildren.Count.Should().Equal(2);
    }

    private class BChild : BNode;

    private class BGrandChild : BChild;

    private class CChild : CNode;

    private sealed class TestListener : Listener<TestContext, TestNode>
    {
        public int Count { get; private set; }

        protected internal override void ListenToNode(TestContext context, TestNode _)
        {
            context.Count++;
            Count++;
        }
    }

    private sealed class TestListener<TNode> : Listener<TestContext, TestNode, TNode>
        where TNode : TestNode
    {
        public Func<TestContext, TNode, bool>? ListenToChildren { get; init; }

        public int Count { get; private set; }

        protected override void ListenToNode(TestContext context, TNode _)
        {
            context.Count++;
            Count++;
        }

        protected override bool ShouldListenToChildren(TestContext context, TNode node) => ListenToChildren?.Invoke(context, node) ?? base.ShouldListenToChildren(context, node);
    }

    private sealed class TestContext
    {
        public int Count { get; set; }
    }
}