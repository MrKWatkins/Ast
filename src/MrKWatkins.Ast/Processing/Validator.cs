namespace MrKWatkins.Ast.Processing;

/// <summary>
/// An <see cref="Processor{TNode}" /> for validating nodes in a tree.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes to validate.</typeparam>
public abstract class Validator<TBaseNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    public sealed override TBaseNode Process(TBaseNode node)
    {
        foreach (var message in Validate(node))
        {
            node.AddMessage(message);
        }

        return node;
    }

    /// <summary>
    /// Validate the node and return any <see cref="Message">Messages</see> to attach to the node to describe any validation issues.
    /// </summary>
    /// <param name="node">The node to validate.</param>
    /// <returns><see cref="Message">Messages</see> to attach to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> Validate(TBaseNode node);
}

/// <summary>
/// An <see cref="Processor{TNode}" /> for validating nodes in a tree.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes to validate.</typeparam>
public abstract class Validator<TContext, TBaseNode> : Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    public sealed override TBaseNode Process(TContext context, TBaseNode node)
    {
        foreach (var message in Validate(context, node))
        {
            node.AddMessage(message);
        }

        return node;
    }

    /// <summary>
    /// Validate the node and return any <see cref="Message">Messages</see> to attach to the node to describe any validation issues.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="node">The node to validate.</param>
    /// <returns><see cref="Message">Messages</see> to attach to the node.</returns>
    [Pure]
    protected abstract IEnumerable<Message> Validate(TContext context, TBaseNode node);
}