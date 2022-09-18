namespace MrKWatkins.Ast.Processing;

public abstract class ProcessingException : Exception
{
    private protected ProcessingException(string message)
        : base(message)
    {
    }
    
    internal ProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public sealed class ProcessingException<TNode> : ProcessingException
    where TNode : Node<TNode>
{
    internal ProcessingException(string message, TNode node)
        : base(message)
    {
        Node = node;
    }
    
    internal ProcessingException(string message, Exception innerException, TNode node)
        : base(message, innerException)
    {
        Node = node;
    }
    
    public TNode Node { get; }

    public override string Message => $"{base.Message} (Node '{Node}')";
}