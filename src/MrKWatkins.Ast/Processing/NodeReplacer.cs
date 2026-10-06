namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="OrderedNodeProcessor{TBaseNode, TNode}" /> for optionally replacing nodes of a specific type in a tree.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to replace.</typeparam>
public abstract class NodeReplacer<TBaseNode, TNode> : OrderedNodeProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected sealed override TBaseNode Process(TNode node) => Replacement.Apply(node, Replace(node));

    /// <summary>
    /// Optionally replace the specified node.
    /// </summary>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// The replacement node, or <paramref name="node" /> or <c>null</c> to leave it alone. The replacement must not already have a parent.
    /// </returns>
    [Pure]
    protected abstract TBaseNode? Replace(TNode node);
}

/// <summary>
/// An <see cref="OrderedNodeProcessor{TContext, TBaseNode, TNode}" /> for optionally replacing nodes of a specific type in a tree.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to replace.</typeparam>
public abstract class NodeReplacer<TContext, TBaseNode, TNode> : OrderedNodeProcessor<TContext, TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected sealed override TBaseNode Process(TContext context, TNode node) => Replacement.Apply(node, Replace(context, node));

    /// <summary>
    /// Optionally replace the specified node.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// The replacement node, or <paramref name="node" /> or <c>null</c> to leave it alone. The replacement must not already have a parent.
    /// </returns>
    [Pure]
    protected abstract TBaseNode? Replace(TContext context, TNode node);
}