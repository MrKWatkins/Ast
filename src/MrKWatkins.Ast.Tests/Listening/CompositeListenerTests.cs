using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class CompositeListenerTests : TreeTestFixture
{
    [Test]
    public void With_ThrowsIfListenerForTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestNode>
            .Build()
            .With(new TestListener<ANode>());

        builder.Invoking(b => b.With(new TestListener<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A listener has already been registered for ANode.");
    }
    
    [Test]
    public void With_ThrowsIfListenerForRootTypeAlreadyRegistered()
    {
        var builder = CompositeListener<TestNode>
            .Build()
            .With(new TestListener());

        builder.Invoking(b => b.With(new TestListener()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A listener has already been registered for TestNode.");
    }
    
    [Test]
    public void Listen_NoSubTypes()
    {
        var aListener = new TestListener<ANode>();
        var bChildListener = new TestListener<BChild>();
        var cListener = new TestListener<CNode>();

        var listener = CompositeListener<TestNode>
            .Build()
            .With(aListener)
            .With(bChildListener)
            .With(cListener)
            .ToListener();

        listener.Listen(N1);

        aListener.Count.Should().Be(4);
        bChildListener.Count.Should().Be(0);
        cListener.Count.Should().Be(2);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(N1);

        aListener.Count.Should().Be(8);
        bChildListener.Count.Should().Be(0);
        cListener.Count.Should().Be(4);
    }
    
    [Test]
    public void Listen_SubType()
    {
        var bListener = new TestListener<BNode>();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(bListener)
            .ToListener();

        listener.Listen(tree);
        
        bListener.Count.Should().Be(3);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(tree);
        
        bListener.Count.Should().Be(6);
    }
    
    [Test]
    public void Listen_MostSpecificSubTypeHandlerUser()
    {
        var bListener = new TestListener<BNode>();
        var bChildListener = new TestListener<BChild>();

        var tree = new ANode(new BChild(), new BGrandChild());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(bListener)
            .With(bChildListener)
            .ToListener();

        listener.Listen(tree);
        
        bListener.Count.Should().Be(0);
        bChildListener.Count.Should().Be(2);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(tree);
        
        bListener.Count.Should().Be(0);
        bChildListener.Count.Should().Be(4);
    }
    
    [Test]
    public void Listen_RootType()
    {
        var rootListener = new TestListener();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListener<TestNode>
            .Build()
            .With(rootListener)
            .ToListener();

        listener.Listen(tree);
        
        rootListener.Count.Should().Be(5);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(tree);
        
        rootListener.Count.Should().Be(10);
    }

    private class BChild : BNode
    {
    }

    private class BGrandChild : BChild
    {
    }
    
    private class CChild : CNode
    {
    }

    private sealed class TestListener : Listener<TestNode> 
    {
        public int Count { get; private set; }

        protected internal override void ListenToNode(TestNode node) => Count++;
    }
    
    private sealed class TestListener<TNode> : Listener<TestNode, TNode> 
        where TNode : TestNode
    {
        public int Count { get; private set; }

        protected internal override void ListenToNode(TNode node) => Count++;
    }
}