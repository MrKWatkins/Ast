using System.Text;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class ListenerWithContextTests : TreeTestFixture
{
    [TestCase(typeof(TestListenerWithContext), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]
    [TestCase(typeof(CallsBaseTestListenerWithContext), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]    // Explicitly test the base methods do nothing.
    [TestCase(typeof(TypedTestListenerWithContext), "(N1(N11(N111))(N121))")]
    [TestCase(typeof(CallsBaseTypedTestListenerWithContext), "(N1(N11(N111))(N121))")]
    public void Listen(Type listenerType, string expected)
    {
        var listener = (ListenerWithContext<StringBuilder, TestNode>) Activator.CreateInstance(listenerType)!;

        var context = new StringBuilder();
        
        listener.Listen(context, N1);

        context.ToString().Should().Be(expected);
    }
    
    private sealed class TestListenerWithContext : ListenerWithContext<StringBuilder, TestNode>
    {
        protected internal override void BeforeListenToNode(StringBuilder context, TestNode node) => context.Append('(');

        protected internal override void ListenToNode(StringBuilder context, TestNode node) => context.Append(node.Name);

        protected internal override void AfterListenToNode(StringBuilder context, TestNode node) => context.Append(')');
    }
    
    private sealed class CallsBaseTestListenerWithContext : ListenerWithContext<StringBuilder, TestNode>
    {
        protected internal override void BeforeListenToNode(StringBuilder context, TestNode node)
        {
            base.BeforeListenToNode(context, node);
            context.Append('(');
        }

        protected internal override void ListenToNode(StringBuilder context, TestNode node)
        {
            base.ListenToNode(context, node);
            context.Append(node.Name);
        }

        protected internal override void AfterListenToNode(StringBuilder context, TestNode node)
        {
            base.AfterListenToNode(context, node);
            context.Append(')');
        }
    }
    
    private sealed class TypedTestListenerWithContext : ListenerWithContext<StringBuilder, TestNode, ANode>
    {
        protected internal override void BeforeListenToNode(StringBuilder context, ANode node) => context.Append('(');

        protected internal override void ListenToNode(StringBuilder context, ANode node) => context.Append(node.Name);

        protected internal override void AfterListenToNode(StringBuilder context, ANode node) => context.Append(')');
    }
    
    private sealed class CallsBaseTypedTestListenerWithContext : ListenerWithContext<StringBuilder, TestNode, ANode>
    {
        protected internal override void BeforeListenToNode(StringBuilder context, ANode node)
        {
            base.BeforeListenToNode(context, node);
            context.Append('(');
        }

        protected internal override void ListenToNode(StringBuilder context, ANode node)
        {
            base.ListenToNode(context, node);
            context.Append(node.Name);
        }

        protected internal override void AfterListenToNode(StringBuilder context, ANode node)
        {
            base.AfterListenToNode(context, node);
            context.Append(')');
        }
    }
}