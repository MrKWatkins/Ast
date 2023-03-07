namespace MrKWatkins.Ast.Processing;

public abstract class Validator<TNode> : UnorderedProcessor<TNode> 
    where TNode : Node<TNode>
{
    protected sealed override void ProcessNode(TNode node)
    {
        foreach (var message in ValidateNode(node))
        {
            node.AddMessage(message);
        }
    }

    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);
}

public abstract class Validator<TBaseNode, TNode> : UnorderedProcessor<TBaseNode, TNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected sealed override void ProcessNode(TNode node)
    {
        foreach (var message in ValidateNode(node))
        {
            node.AddMessage(message);
        }
    }

    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);
}