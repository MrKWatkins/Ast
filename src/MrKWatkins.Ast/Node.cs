using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

/// <summary>
/// Abstract base class for nodes in a tree.
/// </summary>
/// <typeparam name="TNode">Self generic node parameter.</typeparam>
public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class.
    /// </summary>
    protected Node()
    {
        Children = new Children<TNode>(This);
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class with the specified children.
    /// </summary>
    /// <param name="children">The children to add.</param>
    /// <exception cref="InvalidOperationException">If any of <see cref="Children" /> already have a <see cref="Parent" />.</exception>
    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        Children = new Children<TNode>(This, children);
    }

    private TNode This => (TNode) this;

    /// <summary>
    /// The position of the node in the source code.
    /// </summary>
    public virtual SourcePosition SourcePosition { get; set; } = SourcePosition.None;

    /// <summary>
    /// Returns a string that represents the current node. Defaults to the name of the type of the node.
    /// </summary>
    /// <returns>A string that represents the current node.</returns>
    public override string ToString() => GetType().Name;
}