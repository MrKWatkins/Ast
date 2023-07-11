using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes in a tree in a specified order.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class OrderedProcessor<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root) => ProcessorState<TNode>.Create(ShouldProcessNode, ProcessNode);

    /// <summary>
    /// Override this property to specify the <see cref="ITraversal{TNode}" /> to use to traverse the tree. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}" />.
    /// </summary>
    protected virtual ITraversal<TNode> Traversal => DepthFirstPreOrderTraversal<TNode>.Instance;

    private protected sealed override IEnumerable<TNode> EnumerateNodes(ProcessorState<TNode> state, TNode root) =>
        Traversal.Enumerate(
            root,
            true,
            node => state.Exceptions.Trap(node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    /// <summary>
    /// Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if children should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessChildren(TNode node) => true;

    /// <summary>
    /// Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if <paramref name="node"/> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;

    /// <summary>
    /// Process the specified node.
    /// </summary>
    /// <param name="node">The node to process.</param>
    protected abstract void ProcessNode(TNode node);
}

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes of a specific type in a tree in a specified order.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class OrderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root) => ProcessorState<TBaseNode>.Create<TNode>(ShouldProcessNode, ProcessNode);

    /// <summary>
    /// Override this property to specify the <see cref="ITraversal{TNode}" /> to use to traverse the tree. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}" />.
    /// </summary>
    protected virtual ITraversal<TBaseNode> Traversal => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    private protected sealed override IEnumerable<TBaseNode> EnumerateNodes(ProcessorState<TBaseNode> state, TBaseNode root) =>
        Traversal.Enumerate(
            root,
            true,
            node => state.Exceptions.Trap<TBaseNode, TBaseNode>(node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    /// <summary>
    /// Override this method to optionally decide whether to process the children of the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if children should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessChildren(TBaseNode node) => true;

    /// <summary>
    /// Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns><c>true</c> if <paramref name="node"/> should be processed, <c>false</c> otherwise.</returns>
    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;

    /// <summary>
    /// Process the specified node.
    /// </summary>
    /// <param name="node">The node to process.</param>
    protected abstract void ProcessNode(TNode node);
}