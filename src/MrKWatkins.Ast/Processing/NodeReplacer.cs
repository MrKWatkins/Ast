namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="OrderedNodeProcessor{TBaseNode, TNode}" /> for optionally replacing nodes of a specific type in a tree.
/// </summary>
/// <remarks>
/// A replacer can replace the root of the tree when run in a pipeline, with the new root being returned from the pipeline. Calling
/// <see cref="OrderedNodeProcessor{TBaseNode, TNode}.Process(TBaseNode)" /> directly on a root node that would be replaced throws an
/// <see cref="InvalidOperationException" /> as there is nothing to receive the new root.
/// </remarks>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to replace.</typeparam>
public abstract class NodeReplacer<TBaseNode, TNode> : OrderedNodeProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> is the root of the tree and would be replaced.</exception>
    protected sealed override void Process(TNode node) => Replacement.ApplyOutsidePipeline(node, Replace(node));

    internal sealed override TBaseNode? ProcessInPipeline(NoContext context, TBaseNode node) =>
        node is TNode typedNode ? Replacement.Apply(typedNode, Replace(typedNode)) : null;

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
/// <remarks>
/// A replacer can replace the root of the tree when run in a pipeline, with the new root being returned from the pipeline. Calling
/// <see cref="OrderedNodeProcessor{TContext, TBaseNode, TNode}.Process(TContext, TBaseNode)" /> directly on a root node that would be replaced throws an
/// <see cref="InvalidOperationException" /> as there is nothing to receive the new root.
/// </remarks>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to replace.</typeparam>
public abstract class NodeReplacer<TContext, TBaseNode, TNode> : OrderedNodeProcessor<TContext, TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> is the root of the tree and would be replaced.</exception>
    protected sealed override void Process(TContext context, TNode node) => Replacement.ApplyOutsidePipeline(node, Replace(context, node));

    internal sealed override TBaseNode? ProcessInPipeline(TContext context, TBaseNode node) =>
        node is TNode typedNode ? Replacement.Apply(typedNode, Replace(context, typedNode)) : null;

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