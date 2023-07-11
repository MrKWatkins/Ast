namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Exception thrown by <see cref="Processor{TNode}">Processors</see> when a problem occurs.
/// </summary>
public class ProcessingException : Exception
{
    internal ProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown by <see cref="Processor{TNode}">Processors</see> when a problem occurs with details of the node that caused the problem.
/// </summary>
public sealed class ProcessingException<TNode> : ProcessingException
    where TNode : Node<TNode>
{
    internal ProcessingException(string message, Exception innerException, TNode node)
        : base(message, innerException)
    {
        Node = node;
    }

    /// <summary>
    /// The node that caused the problem.
    /// </summary>
    public TNode Node { get; }

    /// <inheritdoc />
    public override string Message => $"{base.Message} (Node '{Node}')";
}