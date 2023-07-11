namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes in a tree without caring about the order they are processed in.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class UnorderedProcessor<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root) => ProcessorState<TNode>.Create(ShouldProcessNode, ProcessNode);

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
/// A <see cref="Processor{TNode}" /> that processes the nodes of a specific type in a tree without caring about the order they are processed in.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class UnorderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root) => ProcessorState<TBaseNode>.Create<TNode>(ShouldProcessNode, ProcessNode);

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