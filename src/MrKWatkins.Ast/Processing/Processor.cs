namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node in a <see cref="Pipeline{TBaseNode}" />. Does not take a processing context.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class Processor<TBaseNode> : Processor<NoContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    public sealed override void Process(NoContext context, TBaseNode node) => Process(node);

    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendants.
    /// </summary>
    /// <param name="node">The node to process.</param>
    public abstract void Process(TBaseNode node);
}

/// <summary>
/// Performs some processing on a given node using a processing context in a <see cref="Pipeline{TContext, TBaseNode}" />.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendants.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to process.</param>
    public abstract void Process(TContext context, TBaseNode node);

    /// <summary>
    /// Called by a pipeline to process a node. Processes the node and returns the new root of the tree if the root was replaced,
    /// which only replacers can do, otherwise <c>null</c>.
    /// </summary>
    internal virtual TBaseNode? ProcessInPipeline(TContext context, TBaseNode node)
    {
        Process(context, node);
        return null;
    }
}