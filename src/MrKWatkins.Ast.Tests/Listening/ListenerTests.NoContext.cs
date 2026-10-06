using System.Text;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed partial class ListenerTests
{
    [TestCase(typeof(NoContextTestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]
    [TestCase(typeof(CallsBaseNoContextTestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")] // Explicitly test the base methods do nothing.
    [TestCase(typeof(NoContextTypedTestListener), "(N1(N11(N111))(N121))")]
    [TestCase(typeof(CallsBaseNoContextTypedTestListener), "(N1(N11(N111))(N121))")]
    public void NoContext_Listen(Type listenerType, string expected)
    {
        var listener = (Listener<TestNode>) Activator.CreateInstance(listenerType)!;

        listener.Listen(N1);

        listener.ToString().Should().Equal(expected);
    }

    [Test]
    public void NoContext_ShouldListenToDescendants()
    {
        var listener = new NoContextTestListener { ListenToChildren = node => node is not BNode };

        listener.Listen(N1);

        listener.ToString().Should().Equal("(N1(N11(N111))(N12)(N13))");
    }

    private sealed class NoContextTestListener : Listener<TestNode>
    {
        private readonly StringBuilder output = new();

        public Func<TestNode, bool>? ListenToChildren { get; init; }

        protected internal override void BeforeListenToNode(TestNode node) => output.Append('(');

        protected internal override void ListenToNode(TestNode node) => output.Append(node.Name);

        protected internal override void AfterListenToNode(TestNode node) => output.Append(')');

        protected internal override bool ShouldListenToDescendants(TestNode node) => ListenToChildren?.Invoke(node) ?? base.ShouldListenToDescendants(node);

        public override string ToString() => output.ToString();
    }

    private sealed class CallsBaseNoContextTestListener : Listener<TestNode>
    {
        private readonly StringBuilder output = new();
        private readonly Stack<TestNode> stack = new();

        protected internal override void BeforeListenToNode(TestNode node)
        {
            stack.Push(node);
            base.BeforeListenToNode(node);
            output.Append('(');
        }

        protected internal override void ListenToNode(TestNode node)
        {
            stack.Peek().Should().BeTheSameInstanceAs(node);
            base.ListenToNode(node);
            output.Append(node.Name);
        }

        protected internal override void AfterListenToNode(TestNode node)
        {
            stack.Pop().Should().BeTheSameInstanceAs(node);
            base.AfterListenToNode(node);
            output.Append(')');
        }

        public override string ToString() => output.ToString();
    }

    private sealed class NoContextTypedTestListener : NodeListener<TestNode, ANode>
    {
        private readonly StringBuilder output = new();
        private readonly Stack<ANode> stack = new();

        protected override void BeforeListenToNode(ANode node)
        {
            stack.Push(node);
            output.Append('(');
        }

        protected override void ListenToNode(ANode node)
        {
            stack.Peek().Should().BeTheSameInstanceAs(node);
            output.Append(node.Name);
        }

        protected override void AfterListenToNode(ANode node)
        {
            stack.Pop().Should().BeTheSameInstanceAs(node);
            output.Append(')');
        }

        public override string ToString() => output.ToString();
    }

    private sealed class CallsBaseNoContextTypedTestListener : NodeListener<TestNode, ANode>
    {
        private readonly StringBuilder output = new();

        protected override void BeforeListenToNode(ANode node)
        {
            base.BeforeListenToNode(node);
            output.Append('(');
        }

        protected override void ListenToNode(ANode node)
        {
            base.ListenToNode(node);
            output.Append(node.Name);
        }

        protected override void AfterListenToNode(ANode node)
        {
            base.AfterListenToNode(node);
            output.Append(')');
        }

        protected override bool ShouldListenToDescendants(ANode node) => base.ShouldListenToDescendants(node);

        public override string ToString() => output.ToString();
    }
}