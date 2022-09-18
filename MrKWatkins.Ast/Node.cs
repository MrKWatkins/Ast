using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private Properties? properties;

    protected Node()
    {
    }
        
    private TNode This => (TNode) this;
    
    protected Properties Properties => properties ??= new Properties();

    public SourcePosition SourcePosition
    {
        get => Properties.GetOrDefault(nameof(SourcePosition), SourcePosition.None);
        set => Properties.Set(nameof(SourcePosition), value);
    }
    
    [Pure]
    public TNode Copy() => Copy(NodeFactory<TNode>.Default);
        
    [Pure]
    public TNode Copy(INodeFactory<TNode> nodeFactory)
    {
        var copy = nodeFactory.Create(GetType());
        copy.properties = properties?.Copy();
        if (children != null)
        {
            copy.Children.Add(children.Select(c => c.Copy(nodeFactory)));
        }
        return copy;
    }

    public override string ToString() => GetType().Name;
}