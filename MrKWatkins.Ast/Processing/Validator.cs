namespace MrKWatkins.Ast.Processing;

public abstract class Validator<TNode> : Processor<TNode> 
    where TNode : Node<TNode>
{
    protected internal sealed override void ProcessNode(TNode node)
    {
        foreach (var message in ValidateNode(node))
        {
            node.AddMessage(message);
        }
    }

    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);
}

public abstract class Validator<TBaseNode, TNode> : Validator<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    [Pure]
    protected sealed override IEnumerable<Message> ValidateNode(TBaseNode node) => ValidateNode((TNode) node);
    
    [Pure]
    protected abstract IEnumerable<Message> ValidateNode(TNode node);

    protected internal sealed override bool ShouldProcessNode(TBaseNode node) => node is TNode typedNode && ShouldProcessNode(typedNode);

    protected virtual bool ShouldProcessNode(TNode node) => true;
}