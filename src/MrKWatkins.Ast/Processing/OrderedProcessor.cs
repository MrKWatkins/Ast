using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node in a <see cref="Pipeline{Node}" />. The processor can specify the order the pipeline
/// should traverse the tree and whether to process descendents or not.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessor<TBaseNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Gets the traversal that the <see cref="Pipeline{Node}" /> should use for this processor.
    /// </summary>
    /// <param name="root">The root of the tree.</param>
    /// <returns>The traversal to use.</returns>
    public virtual ITraversal<TBaseNode> GetTraversal(TBaseNode root) => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    /// <summary>
    /// Whether descendents of this node should be processed by the <see cref="Pipeline{Node}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendents of <paramref name="node" /> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    public virtual bool ShouldProcessDescendents(TBaseNode node) => true;
}

/// <summary>
/// Performs some processing on a given node using a processing context in a <see cref="Pipeline{Node}" />. The processor can
/// specify the order the pipeline should traverse the tree and whether to process descendents or not.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessor<TContext, TBaseNode> : Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Gets the traversal that the <see cref="Pipeline{Node}" /> should use for this processor.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root of the tree.</param>
    /// <returns>The traversal to use.</returns>
    public virtual ITraversal<TBaseNode> GetTraversal(TContext context, TBaseNode root) => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    /// <summary>
    /// Whether descendents of this node should be processed by the <see cref="Pipeline{Node}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendents of <paramref name="node" /> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    public virtual bool ShouldProcessDescendents(TContext context, TBaseNode node) => true;
}