using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Processing;

public abstract class OrderedProcessorWithContext<TContext, TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    internal override ProcessorState<TNode> CreateState(TNode root)
    {
        var context = CatchAndRethrowExceptions(root, nameof(CreateContext), CreateContext);
        
        return ProcessorState<TNode>.CreateWithContext(context, ShouldProcessNode, ProcessNode);
    }
    
    [Pure]
    protected abstract TContext CreateContext(TNode root);

    protected virtual IDescendentEnumerator<TNode> Enumerator => DepthFirstPreOrder<TNode>.Instance;

    private protected sealed override IEnumerable<TNode> EnumerateNodes(ProcessorState<TNode> state, TNode root) =>
        Enumerator.Enumerate(
            root, 
            true, 
            node => state.Exceptions.Trap(state.GetContext<TContext>(), node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    [Pure]
    protected virtual bool ShouldProcessChildren(TContext context, TNode node) => true;

    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;
    
    protected abstract void ProcessNode(TContext context, TNode node);
}

public abstract class OrderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
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

    protected virtual IDescendentEnumerator<TBaseNode> Enumerator => DepthFirstPreOrder<TBaseNode>.Instance;

    private protected sealed override IEnumerable<TBaseNode> EnumerateNodes(ProcessorState<TBaseNode> state, TBaseNode root) =>
        Enumerator.Enumerate(
            root, 
            true, 
            node => state.Exceptions.Trap(state.GetContext<TContext>(), node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    [Pure]
    protected virtual bool ShouldProcessChildren(TContext context, TBaseNode node) => true;

    [Pure]
    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;
    
    protected abstract void ProcessNode(TContext context, TNode node);
}