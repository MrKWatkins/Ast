namespace MrKWatkins.Ast.Tests;

public abstract class TestNode : PropertyNode<TestNode>
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

    public string Name
    {
        get => Properties.GetOrThrow<string>(nameof(Name));
        set => Properties.Set(nameof(Name), value);
    }

    public override string ToString() => Properties.GetOrDefault<string>(nameof(Name)) ?? base.ToString();
}