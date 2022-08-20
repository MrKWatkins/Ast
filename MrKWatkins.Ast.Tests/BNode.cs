namespace MrKWatkins.Ast.Tests;

public sealed class BNode : TestNode
{
    public BNode()
    {
    }
        
    public BNode(params TestNode[] children)
        : base(children)
    {
    }
}