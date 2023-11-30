using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

/// <summary>
/// Abstract base class for nodes in a tree.
/// </summary>
/// <typeparam name="TNode">Self generic node parameter.</typeparam>
public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private Properties? properties;

    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class.
    /// </summary>
    protected Node()
    {
        Children = new Children<TNode>(This);
    }

    private TNode This => (TNode)this;

    /// <summary>
    /// The <see cref="MrKWatkins.Ast.Properties" /> associated with this node.
    /// </summary>
    protected Properties Properties => properties ??= new Properties();

    /// <summary>
    /// The position of the node in the source code.
    /// </summary>
    public virtual SourcePosition SourcePosition
    {
        get => Properties.GetOrDefault(nameof(SourcePosition), SourcePosition.None);
        set => Properties.Set(nameof(SourcePosition), value);
    }

    /// <summary>
    /// Copies this node using the <see cref="DefaultNodeFactory{TNode}" />.
    /// </summary>
    /// <returns>A copy of this node.</returns>
    [Pure]
    public TNode Copy() => Copy(DefaultNodeFactory<TNode>.Instance);

    /// <summary>
    /// Copies this node using the specified <see cref="INodeFactory{TNode}" />.
    /// </summary>
    /// <returns>A copy of this node.</returns>
    [Pure]
    public TNode Copy(INodeFactory<TNode> nodeFactory)
    {
        var copy = nodeFactory.Create(GetType());
        copy.properties = properties?.Copy();
        copy.Children.Add(Children.Select(c => c.Copy(nodeFactory)));
        return copy;
    }

    /// <inheritdoc />
    public override string ToString() => GetType().Name;
}