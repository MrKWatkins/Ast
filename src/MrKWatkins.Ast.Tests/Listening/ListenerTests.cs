using System.Text;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Tests.Listening;

public sealed class ListenerTests : TreeTestFixture
{
    [TestCase(typeof(TestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]
    [TestCase(typeof(CallsBaseTestListener), "(N1(N11(N111))(N12(N121)(N122)(N123))(N13))")]    // Explicitly test the base methods do nothing.
    [TestCase(typeof(TypedTestListener), "(N1(N11(N111))(N121))")]
    [TestCase(typeof(CallsBaseTypedTestListener), "(N1(N11(N111))(N121))")]
    public void Listen(Type listenerType, string expected)
    {
        var listener = (Listener<TestNode>) Activator.CreateInstance(listenerType)!;
        
        listener.Listen(N1);

        listener.ToString().Should().Be(expected);
    }
    
    private sealed class TestListener : Listener<TestNode>
    {
        private readonly StringBuilder stringBuilder = new();
        
        protected internal override void BeforeListenToNode(TestNode node) => stringBuilder.Append('(');

        protected internal override void ListenToNode(TestNode node) => stringBuilder.Append(node.Name);

        protected internal override void AfterListenToNode(TestNode node) => stringBuilder.Append(')');

        public override string ToString() => stringBuilder.ToString();
    }
    
    private sealed class CallsBaseTestListener : Listener<TestNode>
    {
        private readonly StringBuilder stringBuilder = new();
        
        protected internal override void BeforeListenToNode(TestNode node)
        {
            base.BeforeListenToNode(node);
            stringBuilder.Append('(');
        }

        protected internal override void ListenToNode(TestNode node)
        {
            base.ListenToNode(node);
            stringBuilder.Append(node.Name);
        }

        protected internal override void AfterListenToNode(TestNode node)
        {
            base.AfterListenToNode(node);
            stringBuilder.Append(')');
        }

        public override string ToString() => stringBuilder.ToString();
    }
    
    private sealed class TypedTestListener : Listener<TestNode, ANode>
    {
        private readonly StringBuilder stringBuilder = new();

        protected override void BeforeListenToNode(ANode node) => stringBuilder.Append('(');

        protected override void ListenToNode(ANode node) => stringBuilder.Append(node.Name);

        protected override void AfterListenToNode(ANode node) => stringBuilder.Append(')');

        public override string ToString() => stringBuilder.ToString();
    }
    
    private sealed class CallsBaseTypedTestListener : Listener<TestNode, ANode>
    {
        private readonly StringBuilder stringBuilder = new();

        protected override void BeforeListenToNode(ANode node)
        {
            base.BeforeListenToNode(node);
            stringBuilder.Append('(');
        }

        protected override void ListenToNode(ANode node)
        {
            base.ListenToNode(node);
            stringBuilder.Append(node.Name);
        }

        protected override void AfterListenToNode(ANode node)
        {
            base.AfterListenToNode(node);
            stringBuilder.Append(')');
        }

        public override string ToString() => stringBuilder.ToString();
    }
}