namespace MrKWatkins.Ast.Tests;

public sealed class BNode : TestNode
{
    // Internal to test NodeFactory.Default can run internal constructors.
    [UsedImplicitly]
    internal BNode()
    {
    }

    public BNode([InstantHandle] IEnumerable<TestNode> children)
        : base(children)
    {
    }
        
    public BNode(params TestNode[] children)
        : base(children)
    {
    }

    public override TestNodeType NodeType => TestNodeType.B;
}