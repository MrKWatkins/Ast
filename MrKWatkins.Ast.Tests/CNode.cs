namespace MrKWatkins.Ast.Tests;

public sealed class CNode : TestNode
{
    public CNode()
    {
    }
        
    public CNode([InstantHandle] IEnumerable<TestNode> children)
        : base(children)
    {
    }
        
    public CNode(params TestNode[] children)
        : base(children)
    {
    }

    public override TestNodeType NodeType => TestNodeType.C;
}