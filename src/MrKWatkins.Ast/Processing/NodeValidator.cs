namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="NodeProcessor{TBaseNode, TNode}" /> for validating nodes of a specific type in a tree. Any <see cref="Message" />s returned by validation
/// are added to the node.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to validate.</typeparam>
public abstract class NodeValidator<TBaseNode, TNode> : NodeProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected sealed override void Process(TNode node)
    {
        foreach (var message in Validate(node))
        {
            node.AddMessage(message);
        }
    }

    /// <summary>
    /// Validates the specified node.
    /// </summary>
    /// <param name="node">The node to validate.</param>
    /// <returns>Any <see cref="Message" />s to add to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> Validate(TNode node);
}

/// <summary>
/// A <see cref="NodeProcessor{TContext, TBaseNode, TNode}" /> for validating nodes of a specific type in a tree. Any <see cref="Message" />s returned by
/// validation are added to the node.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to validate.</typeparam>
public abstract class NodeValidator<TContext, TBaseNode, TNode> : NodeProcessor<TContext, TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected sealed override void Process(TContext context, TNode node)
    {
        foreach (var message in Validate(context, node))
        {
            node.AddMessage(message);
        }
    }

    /// <summary>
    /// Validates the specified node.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to validate.</param>
    /// <returns>Any <see cref="Message" />s to add to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> Validate(TContext context, TNode node);
}