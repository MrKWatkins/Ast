namespace MrKWatkins.Ast.Tests;

public sealed class ANode : TestNode
{
    public ANode()
    {
    }

    public ANode([InstantHandle] IEnumerable<TestNode> children)
        : base(children)
    {
    }

    public ANode(params TestNode[] children)
        : base(children)
    {
    }
}