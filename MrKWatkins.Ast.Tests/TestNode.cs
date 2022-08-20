namespace MrKWatkins.Ast.Tests;

public abstract class TestNode : Node<TestNode>
{
    protected TestNode()
    {
    }
    
    protected TestNode([InstantHandle] IEnumerable<TestNode> children)
        : base(children)
    {
    }
        
    protected TestNode(params TestNode[] children)
        : base(children)
    {
    }

    public string TestProperty
    {
        get => Properties.GetOrThrow<string>(nameof(TestProperty));
        set => Properties.Set(nameof(TestProperty), value);
    }
}