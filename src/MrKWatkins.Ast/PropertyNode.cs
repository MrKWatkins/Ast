namespace MrKWatkins.Ast;

/// <summary>
/// Abstract base class for nodes in a tree with a collection of arbitrary properties that can be copied.
/// </summary>
/// <typeparam name="TNode">Self generic node parameter.</typeparam>
public abstract class PropertyNode<TNode> : Node<TNode>
    where TNode : PropertyNode<TNode>
{
    private Properties? properties;

    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class.
    /// </summary>
    protected PropertyNode()
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class with the specified children.
    /// </summary>
    /// <param name="children">The children to add.</param>
    /// <exception cref="InvalidOperationException">If any of <see cref="Node{TNode}.Children" /> already have a <see cref="Node{TNode}.Parent" />.</exception>
    protected PropertyNode([InstantHandle] params IEnumerable<TNode> children)
        : base(children)
    {
    }

    /// <summary>
    /// The <see cref="MrKWatkins.Ast.Properties" /> associated with this node.
    /// </summary>
    /// <remarks>The properties.</remarks>
    protected Properties Properties => properties ??= new Properties();

    /// <summary>
    /// Copies this node and its <see cref="Properties"/> using the <see cref="DefaultNodeFactory{TNode}" />.
    /// </summary>
    /// <remarks>
    /// <see cref="Node{TNode}.SourcePosition"/> and <see cref="Node{TNode}.Messages"/> are not copied. Copying is designed for
    /// reproducing parts of a tree or a general pattern. As such it doesn't make sense to copy <see cref="Node{TNode}.SourcePosition"/>
    /// because the new nodes will not come from the original place. Similarly any <see cref="Node{TNode}.Messages"/> associated
    /// with the originals will not apply to the copy.
    /// </remarks>
    /// <returns>A copy of this node.</returns>
    [Pure]
    public TNode Copy() => Copy(DefaultNodeFactory<TNode>.Instance);

    /// <summary>
    /// Copies this node and its <see cref="Properties"/> using the specified <see cref="INodeFactory{TNode}" />.
    /// </summary>
    /// <remarks>
    /// <see cref="Node{TNode}.SourcePosition"/> and <see cref="Node{TNode}.Messages"/> are not copied. Copying is designed for
    /// reproducing parts of a tree or a general pattern. As such it doesn't make sense to copy <see cref="Node{TNode}.SourcePosition"/>
    /// because the new nodes will not come from the original place. Similarly any <see cref="Node{TNode}.Messages"/> associated
    /// with the originals will not apply to the copy.
    /// </remarks>
    /// <returns>A copy of this node.</returns>
    [Pure]
    public TNode Copy(INodeFactory<TNode> nodeFactory)
    {
        var copy = nodeFactory.Create(GetType());
        copy.properties = properties?.Copy();
        copy.Children.Add(Children.Select(c => c.Copy(nodeFactory)));
        return copy;
    }
}