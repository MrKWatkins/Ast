namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node in a <see cref="Pipeline{Node}" />. The processor can specify the order the pipeline
/// should traverse the tree and whether to process descendents or not.
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
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="node">The node to process.</param>
    protected abstract void Process(TNode node);

    /// <inheritdoc />
    public sealed override bool ShouldProcessDescendents(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return ShouldProcessDescendents(typedNode);
        }

        return true;
    }

    /// <summary>
    /// Whether descendents of this node should be processed by the <see cref="Pipeline{Node}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendents of <paramref name="node" /> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessDescendents(TNode node) => true;
}

/// <summary>
/// Performs some processing on a given node using a processing context in a <see cref="Pipeline{TContext, Node}" />. The processor can
/// specify the order the pipeline should traverse the tree and whether to process descendents or not.
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
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to process.</param>
    protected abstract void Process(TContext context, TNode node);

    /// <inheritdoc />
    public sealed override bool ShouldProcessDescendents(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return ShouldProcessDescendents(context, typedNode);
        }

        return true;
    }

    /// <summary>
    /// Whether descendents of this node should be processed by the <see cref="Pipeline{TContext, Node}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendents of <paramref name="node" /> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessDescendents(TContext context, TNode node) => true;
}