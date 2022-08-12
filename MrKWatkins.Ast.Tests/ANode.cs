namespace MrKWatkins.Ast.Tests;

public sealed class ANode : TestNode
{
    // Private to test NodeFactory.Default can run private constructors.
    [UsedImplicitly]
    private ANode()
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

    public override TestNodeType NodeType => TestNodeType.A;
}