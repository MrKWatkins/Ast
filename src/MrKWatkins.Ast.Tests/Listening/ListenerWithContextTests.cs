using System.Text;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class ListenerTests : TreeTestFixture
{
    [TestCase(typeof(TestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]
    [TestCase(typeof(CallsBaseTestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")] // Explicitly test the base methods do nothing.
    [TestCase(typeof(TypedTestListener), "(N1(N11(N111))(N121))")]
    [TestCase(typeof(CallsBaseTypedTestListener), "(N1(N11(N111))(N121))")]
    public void Listen(Type listenerType, string expected)
    {
        var listener = (Listener<StringBuilder, TestNode>) Activator.CreateInstance(listenerType)!;

        var context = new StringBuilder();

        listener.Listen(context, N1);

        context.ToString().Should().Be(expected);
    }

    [Test]
    public void ShouldListenToChildren()
    {
        var listener = new TestListener { ListenToChildren = (_, node) => node is not BNode };

        var context = new StringBuilder();

        listener.Listen(context, N1);

        context.ToString().Should().Be("(N1(N11(N111))(N12)(N13))");
    }

    private sealed class TestListener : Listener<StringBuilder, TestNode>
    {
        public Func<StringBuilder, TestNode, bool>? ListenToChildren { get; init; }

        protected internal override void BeforeListenToNode(StringBuilder context, TestNode node) => context.Append('(');

        protected internal override void ListenToNode(StringBuilder context, TestNode node) => context.Append(node.Name);

        protected internal override void AfterListenToNode(StringBuilder context, TestNode node) => context.Append(')');

        protected override bool ShouldListenToChildren(StringBuilder context, TestNode node) => ListenToChildren?.Invoke(context, node) ?? base.ShouldListenToChildren(context, node);
    }

    private sealed class CallsBaseTestListener : Listener<StringBuilder, TestNode>
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

    private sealed class TypedTestListener : Listener<StringBuilder, TestNode, ANode>
    {
        protected override void BeforeListenToNode(StringBuilder context, ANode node) => context.Append('(');

        protected override void ListenToNode(StringBuilder context, ANode node) => context.Append(node.Name);

        protected override void AfterListenToNode(StringBuilder context, ANode node) => context.Append(')');
    }

    private sealed class CallsBaseTypedTestListener : Listener<StringBuilder, TestNode, ANode>
    {
        protected override void BeforeListenToNode(StringBuilder context, ANode node)
        {
            base.BeforeListenToNode(context, node);
            context.Append('(');
        }

        protected override void ListenToNode(StringBuilder context, ANode node)
        {
            base.ListenToNode(context, node);
            context.Append(node.Name);
        }

        protected override void AfterListenToNode(StringBuilder context, ANode node)
        {
            base.AfterListenToNode(context, node);
            context.Append(')');
        }
    }
}