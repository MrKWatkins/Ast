using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class CompositeListenerWithContextTests : TreeTestFixture
{
    [Test]
    public void With_ThrowsIfListenerWithContextForTypeAlreadyRegistered()
    {
        var builder = CompositeListenerWithContext<TestContext, TestNode>
            .Build()
            .With(new TestListenerWithContext<ANode>());

        builder.Invoking(b => b.With(new TestListenerWithContext<ANode>()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A listener has already been registered for ANode.");
    }
    
    [Test]
    public void With_ThrowsIfListenerWithContextForRootTypeAlreadyRegistered()
    {
        var builder = CompositeListenerWithContext<TestContext, TestNode>
            .Build()
            .With(new TestListenerWithContext());

        builder.Invoking(b => b.With(new TestListenerWithContext()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A listener has already been registered for TestNode.");
    }
    
    [Test]
    public void ToListener_ThrowsIfNoListenersRegistered()
    {
        var builder = CompositeListenerWithContext<TestContext, TestNode>.Build();

        builder.Invoking(b => b.ToListener())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("No listeners have been registered.");
    }
    
    [Test]
    public void Listen_NoSubTypes()
    {
        var aListenerWithContext = new TestListenerWithContext<ANode>();
        var bChildListenerWithContext = new TestListenerWithContext<BChild>();
        var cListenerWithContext = new TestListenerWithContext<CNode>();

        var listener = CompositeListenerWithContext<TestContext, TestNode>
            .Build()
            .With(aListenerWithContext)
            .With(bChildListenerWithContext)
            .With(cListenerWithContext)
            .ToListener();

        var context = new TestContext();
        
        listener.Listen(context, N1);

        context.Count.Should().Be(6);
        aListenerWithContext.Count.Should().Be(4);
        bChildListenerWithContext.Count.Should().Be(0);
        cListenerWithContext.Count.Should().Be(2);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(context, N1);

        context.Count.Should().Be(12);
        aListenerWithContext.Count.Should().Be(8);
        bChildListenerWithContext.Count.Should().Be(0);
        cListenerWithContext.Count.Should().Be(4);
    }
    
    [Test]
    public void Listen_SubType()
    {
        var bListenerWithContext = new TestListenerWithContext<BNode>();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListener<TestNode>
            .BuildWithContext<TestContext>()
            .With(bListenerWithContext)
            .ToListener();

        var context = new TestContext();
        
        listener.Listen(context, tree);
        
        context.Count.Should().Be(3);
        bListenerWithContext.Count.Should().Be(3);
        
        // Repeat to ensure cached handlers work.
        context = new TestContext();
        listener.Listen(context, tree);
        
        context.Count.Should().Be(3);
        bListenerWithContext.Count.Should().Be(6);
    }
    
    [Test]
    public void Listen_MostSpecificSubTypeHandlerUser()
    {
        var bListenerWithContext = new TestListenerWithContext<BNode>();
        var bChildListenerWithContext = new TestListenerWithContext<BChild>();

        var tree = new ANode(new BChild(), new BGrandChild());

        var listener = CompositeListenerWithContext<TestContext, TestNode>
            .Build()
            .With(bListenerWithContext)
            .With(bChildListenerWithContext)
            .ToListener();

        var context = new TestContext();
        
        listener.Listen(context, tree);
        
        context.Count.Should().Be(2);
        bListenerWithContext.Count.Should().Be(0);
        bChildListenerWithContext.Count.Should().Be(2);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(context, tree);
        
        context.Count.Should().Be(4);
        bListenerWithContext.Count.Should().Be(0);
        bChildListenerWithContext.Count.Should().Be(4);
    }
    
    [Test]
    public void Listen_RootType()
    {
        var rootListenerWithContext = new TestListenerWithContext();

        var tree = new ANode(new BNode(), new BChild(), new BGrandChild(), new CChild());

        var listener = CompositeListenerWithContext<TestContext, TestNode>
            .Build()
            .With(rootListenerWithContext)
            .ToListener();

        var context = new TestContext();
        listener.Listen(context, tree);
        
        context.Count.Should().Be(5);
        rootListenerWithContext.Count.Should().Be(5);
        
        // Repeat to ensure cached handlers work.
        listener.Listen(context, tree);
        
        context.Count.Should().Be(10);
        rootListenerWithContext.Count.Should().Be(10);
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

    private sealed class TestListenerWithContext : ListenerWithContext<TestContext, TestNode> 
    {
        public int Count { get; private set; }

        protected internal override void ListenToNode(TestContext context, TestNode _)
        {
            context.Count++;
            Count++;
        }
    }
    
    private sealed class TestListenerWithContext<TNode> : ListenerWithContext<TestContext, TestNode, TNode> 
        where TNode : TestNode
    {
        public int Count { get; private set; }

        protected override void ListenToNode(TestContext context, TNode _)
        {
            context.Count++;
            Count++;
        }
    }

    private sealed class TestContext
    {
        public int Count { get; set; }
    }
}