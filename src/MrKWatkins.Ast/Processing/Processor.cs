namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node in a <see cref="Pipeline{Node}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="node">The node to process.</param>
    public abstract void Process(TBaseNode node);
}

/// <summary>
/// Performs some processing on a given node using a processing context in a <see cref="Pipeline{TContext, Node}" />.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Performs processing on the specified <paramref name="node" />. Does not process any descendents.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to process.</param>
    public abstract void Process(TContext context, TBaseNode node);
}