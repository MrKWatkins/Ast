using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Performs some processing on a given node in a <see cref="Pipeline{TBaseNode}" />. The processor can specify the order the pipeline
/// should traverse the tree and whether to process descendants or not. Does not take a processing context.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessor<TBaseNode> : Processor<TBaseNode>, IOrderedProcessor<NoContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Gets the traversal that the <see cref="Pipeline{TBaseNode}" /> should use for this processor.
    /// </summary>
    /// <param name="root">The root of the tree.</param>
    /// <returns>The traversal. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}" />.</returns>
    public virtual ITraversal<TBaseNode> GetTraversal(TBaseNode root) => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    /// <summary>
    /// Whether descendants of this node should be processed by the <see cref="Pipeline{TBaseNode}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendants should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    public virtual bool ShouldProcessDescendants(TBaseNode node) => true;

    ITraversal<TBaseNode> IOrderedProcessor<NoContext, TBaseNode>.GetTraversal(NoContext context, TBaseNode root) => GetTraversal(root);

    bool IOrderedProcessor<NoContext, TBaseNode>.ShouldProcessDescendants(NoContext context, TBaseNode node) => ShouldProcessDescendants(node);
}

/// <summary>
/// Performs some processing on a given node using a processing context in a <see cref="Pipeline{Node}" />. The processor can
/// specify the order the pipeline should traverse the tree and whether to process descendants or not.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessor<TContext, TBaseNode> : Processor<TContext, TBaseNode>, IOrderedProcessor<TContext, TBaseNode>
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
    /// Whether descendants of this node should be processed by the <see cref="Pipeline{Node}" /> or not. Defaults to <c>true</c>.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if descendants of <paramref name="node" /> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    public virtual bool ShouldProcessDescendants(TContext context, TBaseNode node) => true;

    ITraversal<TBaseNode> IOrderedProcessor<TContext, TBaseNode>.GetTraversal(TContext context, TBaseNode root) => GetTraversal(context, root);

    bool IOrderedProcessor<TContext, TBaseNode>.ShouldProcessDescendants(TContext context, TBaseNode node) => ShouldProcessDescendants(context, node);
}