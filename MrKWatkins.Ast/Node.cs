using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private TNode? parent;
    private Children<TNode>? children;
    private Properties? properties;
    private List<Message>? messages;

    protected Node()
    {
    }

    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        this.children = new Children<TNode>(This, children);
    }
        
    private TNode This => (TNode) this;
    
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