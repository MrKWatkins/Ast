namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="Processor{TBaseNode}" /> for validating nodes in a tree. Any <see cref="Message" />s returned by validation are added to the node.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public abstract class Validator<TBaseNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    public sealed override void Process(TBaseNode node)
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
    protected abstract IEnumerable<Message> Validate(TBaseNode node);
}

/// <summary>
/// A <see cref="Processor{TContext, TBaseNode}" /> for validating nodes in a tree. Any <see cref="Message" />s returned by validation are added to the node.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public abstract class Validator<TContext, TBaseNode> : Processor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <inheritdoc />
    public sealed override void Process(TContext context, TBaseNode node)
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
    protected abstract IEnumerable<Message> Validate(TContext context, TBaseNode node);
}