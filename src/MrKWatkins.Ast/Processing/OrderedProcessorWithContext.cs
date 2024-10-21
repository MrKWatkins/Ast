using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes in a tree in a specified order and gives access to a context object during processing.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessorWithContext<TContext, TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root)
    {
        var context = CatchAndRethrowExceptions(root, nameof(CreateContext), CreateContext);

        return ProcessorState<TNode>.CreateWithContext(context, ShouldProcessNode, ProcessNode);
    }

    /// <summary>
    /// Override to create the context object.
    /// </summary>
    /// <param name="root">The root node for the processing.</param>
    /// <returns>The context object.</returns>
    [Pure]
    protected abstract TContext CreateContext(TNode root);

    /// <summary>
    /// Override this property to specify the <see cref="ITraversal{TNode}" /> to use to traverse the tree. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}" />.
    /// </summary>
    protected virtual ITraversal<TNode> Traversal => DepthFirstPreOrderTraversal<TNode>.Instance;

    private protected sealed override IEnumerable<TNode> EnumerateNodes(ProcessorState<TNode> state, TNode root) =>
        Traversal.Enumerate(
            root,
            true,
            node => state.Exceptions.Trap(state.GetContext<TContext>(), node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    /// <summary>
    /// Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if children should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessChildren(TContext context, TNode node) => true;

    /// <summary>
    /// Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if <paramref name="node"/> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;

    /// <summary>
    /// Process the specified node.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node to process.</param>
    protected abstract void ProcessNode(TContext context, TNode node);
}

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes of a specific type in a tree in a specified order and gives access to a
/// context object during processing.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class OrderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root)
    {
        var context = CatchAndRethrowExceptions(root, nameof(CreateContext), CreateContext);

        return ProcessorState<TBaseNode>.CreateWithContext<TContext, TNode>(context, ShouldProcessNode, ProcessNode);
    }

    /// <summary>
    /// Override to create the context object.
    /// </summary>
    /// <param name="root">The root node for the processing.</param>
    /// <returns>The context object.</returns>
    [Pure]
    protected abstract TContext CreateContext(TBaseNode root);

    /// <summary>
    /// Override this property to specify the <see cref="ITraversal{TNode}" /> to use to traverse the tree. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}" />.
    /// </summary>
    [PublicAPI]
    protected virtual ITraversal<TBaseNode> Traversal => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    private protected sealed override IEnumerable<TBaseNode> EnumerateNodes(ProcessorState<TBaseNode> state, TBaseNode root) =>
        Traversal.Enumerate(
            root,
            true,
            node => state.Exceptions.Trap(state.GetContext<TContext>(), node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    /// <summary>
    /// Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if children should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessChildren(TContext context, TBaseNode node) => true;

    /// <summary>
    /// Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if <paramref name="node"/> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;

    /// <summary>
    /// Process the specified node.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node to process.</param>
    protected abstract void ProcessNode(TContext context, TNode node);
}