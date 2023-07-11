namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="Processor{TNode}" /> that processes the nodes in a tree without caring about the order they are processed in and gives access to a
/// context object during processing.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class UnorderedProcessorWithContext<TContext, TNode> : Processor<TNode>
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
/// A <see cref="Processor{TNode}" /> that processes the nodes of a specific type that processes the nodes in a tree without
/// caring about the order they are processed in and gives access to a context object during processing.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class UnorderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
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