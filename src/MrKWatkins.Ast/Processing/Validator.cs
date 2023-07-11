namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="UnorderedProcessor{TNode}" /> for validating nodes in a tree. 
/// </summary>
/// <typeparam name="TNode"></typeparam>
public abstract class Validator<TNode> : UnorderedProcessor<TNode>
    where TNode : Node<TNode>
{
    /// <inheritdoc />
    protected sealed override void ProcessNode(TNode node)
    {
        foreach (var message in ValidateNode(node))
        {
            node.AddMessage(message);
        }
    }

    /// <summary>
    /// Validate the node and return any <see cref="Message">Messages</see> to attach to the node to describe any validation issues.
    /// </summary>
    /// <param name="node">The node to validate.</param>
    /// <returns><see cref="Message">Messages</see> to attach to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);
}

/// <summary>
/// An <see cref="UnorderedProcessor{TNode}" /> for validating nodes of a specific type in a tree. 
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of nodes to process.</typeparam>
public abstract class Validator<TBaseNode, TNode> : UnorderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected sealed override void ProcessNode(TNode node)
    {
        foreach (var message in ValidateNode(node))
        {
            node.AddMessage(message);
        }
    }

    /// <summary>
    /// Validate the node and return any <see cref="Message">Messages</see> to attach to the node to describe any validation issues.
    /// </summary>
    /// <param name="node">The node to validate.</param>
    /// <returns><see cref="Message">Messages</see> to attach to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);
}