namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node of a specific type in a <see cref="Pipeline{Node}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class NodeProcessor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    public sealed override TBaseNode Process(TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return Process(typedNode);
        }

        return node;
    }

    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="node">The node to process.</param>
    /// <returns>The root node of the tree, which may have been replaced.</returns>
    protected abstract TBaseNode Process(TNode node);
}

/// <summary>
/// Performs some processing on a given node of a specific type using a processing context in a <see cref="Pipeline{TContext, Node}" />.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class NodeProcessor<TContext, TBaseNode, TNode> : Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    public sealed override TBaseNode Process(TContext context, TBaseNode node)
    {
        if (node is TNode typedNode)
        {
            return Process(context, typedNode);
        }

        return node;
    }

    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to process.</param>
    /// <returns>The root node of the tree, which may have been replaced.</returns>
    protected abstract TBaseNode Process(TContext context, TNode node);
}