namespace MrKWatkins.Ast.Processing;

public class ProcessingException : Exception
{
    internal ProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public sealed class ProcessingException<TNode> : ProcessingException
    where TNode : Node<TNode>
{
    internal ProcessingException(string message, Exception innerException, TNode node)
        : base(message, innerException)
    {
        Node = node;
    }
    
    public TNode Node { get; }

    public override string Message => $"{base.Message} (Node '{Node}')";
}