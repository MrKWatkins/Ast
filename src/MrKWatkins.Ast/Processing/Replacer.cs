namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="OrderedProcessor{TBaseNode}" /> for optionally replacing nodes in a tree.
/// </summary>
/// <remarks>
/// A replacer can replace the root of the tree when run in a pipeline, with the new root being returned from the pipeline. Calling
/// <see cref="Process" /> directly on a root node that would be replaced throws an <see cref="InvalidOperationException" /> as there
/// is nothing to receive the new root.
/// </remarks>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public abstract class Replacer<TBaseNode> : OrderedProcessor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> is the root of the tree and would be replaced.</exception>
    public sealed override void Process(TBaseNode node) => Replacement.ApplyOutsidePipeline(node, Replace(node));

    internal sealed override TBaseNode? ProcessInPipeline(NoContext context, TBaseNode node) => Replacement.Apply(node, Replace(node));

    /// <summary>
    /// Optionally replace the specified node.
    /// </summary>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// The replacement node, or <paramref name="node" /> or <c>null</c> to leave it alone. The replacement must not already have a parent.
    /// </returns>
    [Pure]
    protected abstract TBaseNode? Replace(TBaseNode node);
}

/// <summary>
/// An <see cref="OrderedProcessor{TContext, TBaseNode}" /> for optionally replacing nodes in a tree.
/// </summary>
/// <remarks>
/// A replacer can replace the root of the tree when run in a pipeline, with the new root being returned from the pipeline. Calling
/// <see cref="Process" /> directly on a root node that would be replaced throws an <see cref="InvalidOperationException" /> as there
/// is nothing to receive the new root.
/// </remarks>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public abstract class Replacer<TContext, TBaseNode> : OrderedProcessor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> is the root of the tree and would be replaced.</exception>
    public sealed override void Process(TContext context, TBaseNode node) => Replacement.ApplyOutsidePipeline(node, Replace(context, node));

    internal sealed override TBaseNode? ProcessInPipeline(TContext context, TBaseNode node) => Replacement.Apply(node, Replace(context, node));

    /// <summary>
    /// Optionally replace the specified node.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to potentially replace.</param>
    /// <returns>
    /// The replacement node, or <paramref name="node" /> or <c>null</c> to leave it alone. The replacement must not already have a parent.
    /// </returns>
    [Pure]
    protected abstract TBaseNode? Replace(TContext context, TBaseNode node);
}