namespace MrKWatkins.Ast.Processing;

public abstract class UnorderedProcessor<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root) => ProcessorState<TNode>.Create(ShouldProcessNode, ProcessNode);

    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;
    
    protected abstract void ProcessNode(TNode node);
}

public abstract class UnorderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root) => ProcessorState<TBaseNode>.Create<TNode>(ShouldProcessNode, ProcessNode);

    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;
    
    protected abstract void ProcessNode(TNode node);
}