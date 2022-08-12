namespace MrKWatkins.Ast.Tests;

public abstract class TestNode : Node<TestNodeType, TestNode>
{
    protected TestNode([InstantHandle] IEnumerable<TestNode> children)
        : base(children)
    {
    }
        
    protected TestNode(params TestNode[] children)
        : base(children)
    {
    }
}