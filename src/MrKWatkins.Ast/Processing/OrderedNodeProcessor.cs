namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node of a specific type in a <see cref="Pipeline{TBaseNode}" />. The processor can specify the order the pipeline
/// should traverse the tree and whether to process descendants or not.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class OrderedNodeProcessor<TBaseNode, TNode> : OrderedProcessor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    public sealed override void Process(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            Process(typedNode);
        }
    }

    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendants.
    /// </summary>
    /// <param name="node">The node to process.</param>
    protected abstract void Process(TNode node);

    /// <inheritdoc />
    public sealed override bool ShouldProcessDescendants(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return ShouldProcessDescendants(typedNode);
        }

        return true;
    }

    /// <summary>
    /// Whether descendants of this node should be processed by the <see cref="Pipeline{TBaseNode}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendants should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessDescendants(TNode node) => true;
}

/// <summary>
/// Performs some processing on a given node of a specific type using a processing context in a <see cref="Pipeline{TContext, TBaseNode}" />. The processor
/// can specify the order the pipeline should traverse the tree and whether to process descendants or not.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class OrderedNodeProcessor<TContext, TBaseNode, TNode> : OrderedProcessor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    public sealed override void Process(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            Process(context, typedNode);
        }
    }

    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendants.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to process.</param>
    protected abstract void Process(TContext context, TNode node);

    /// <inheritdoc />
    public sealed override bool ShouldProcessDescendants(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return ShouldProcessDescendants(context, typedNode);
        }

        return true;
    }

    /// <summary>
    /// Whether descendants of this node should be processed by the <see cref="Pipeline{TContext, TBaseNode}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendants should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessDescendants(TContext context, TNode node) => true;
}