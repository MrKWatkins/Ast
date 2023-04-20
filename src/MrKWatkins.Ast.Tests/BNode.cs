namespace MrKWatkins.Ast.Tests;

public class BNode : TestNode
{
    public BNode()
    {
    }
        
    public BNode(params TestNode[] children)
        : base(children)
    {
    }
}