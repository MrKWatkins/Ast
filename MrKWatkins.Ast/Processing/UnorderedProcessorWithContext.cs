namespace MrKWatkins.Ast.Processing;

public abstract class UnorderedProcessorWithContext<TContext, TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root)
    {
        var context = CatchAndRethrowExceptions(root, nameof(CreateContext), CreateContext);
        
        return ProcessorState<TNode>.CreateWithContext(context, ShouldProcessNode, ProcessNode);
    }

    [Pure]
    protected abstract TContext CreateContext(TNode root);
    
    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;
    
    protected abstract void ProcessNode(TContext context, TNode node);
}

public abstract class UnorderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root)
    {
        var context = CatchAndRethrowExceptions(root, nameof(CreateContext), CreateContext);
        
        return ProcessorState<TBaseNode>.CreateWithContext<TContext, TNode>(context, ShouldProcessNode, ProcessNode);
    }

    [Pure]
    protected abstract TContext CreateContext(TBaseNode root);

    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;
    
    protected abstract void ProcessNode(TContext context, TNode node);
}