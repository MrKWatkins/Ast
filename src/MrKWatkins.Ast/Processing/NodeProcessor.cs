namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node of a specific type in a <see cref="Pipeline{TBaseNode}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class NodeProcessor<TBaseNode, TNode> : Processor<TBaseNode>
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
}

/// <summary>
/// Performs some processing on a given node of a specific type using a processing context in a <see cref="Pipeline{TContext, TBaseNode}" />.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of node to process.</typeparam>
public abstract class NodeProcessor<TContext, TBaseNode, TNode> : Processor<TContext, TBaseNode>
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
}