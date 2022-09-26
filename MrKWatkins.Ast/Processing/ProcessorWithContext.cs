using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Processing;

public abstract class ProcessorWithContext<TContext, TNode> : IProcessor<TNode>
    where TNode : Node<TNode>
{
    [Pure]
    protected abstract TContext CreateContext(TNode root);
    
    public void Process(TNode root)
    {
        TContext context;
        try
        {
            context = CreateContext(root);
        }
        catch (Exception exception)
        {
            throw new ProcessingException($"Exception during {nameof(CreateContext)}.", exception);
        }
        
        foreach (var node in Enumerator.Enumerate(root, true, n => CatchAndRethrowExceptions(context, n, nameof(ShouldProcessChildren), ShouldProcessChildren)))
        {
            if (CatchAndRethrowExceptions(context, node, nameof(ShouldProcessNode), ShouldProcessNode))
            {
                CatchAndRethrowExceptions(context, node, nameof(ProcessNode), ProcessNode);
            }
        }
    }
    
    protected internal virtual IDescendentEnumerator<TNode> Enumerator => DepthFirstPreOrder<TNode>.Instance;

    [Pure]
    protected internal virtual bool ShouldProcessNode(TContext context, TNode node) => true;
    
    [Pure]
    protected internal virtual bool ShouldProcessChildren(TContext context, TNode node) => true;

    protected internal abstract void ProcessNode(TContext context, TNode node);

    [Pure]
    private static TResult CatchAndRethrowExceptions<TResult>(TContext context, TNode node, string method, Func<TContext, TNode, TResult> function)
    {
        try
        {
            return function(context, node);
        }
        catch (Exception exception)
        {
            throw new ProcessingException<TNode>($"Exception during {method}.", exception, node);
        }
    }
    
    private static void CatchAndRethrowExceptions(TContext context, TNode node, string method, Action<TContext, TNode> action)
    {
        try
        {
            action(context, node);
        }
        catch (Exception exception)
        {
            throw new ProcessingException<TNode>($"Exception during {method}.", exception, node);
        }
    }
}

public abstract class ProcessorWithContext<TContext, TBaseNode, TNode> : ProcessorWithContext<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected internal sealed override bool ShouldProcessNode(TContext context, TBaseNode node) => node is TNode typedNode && ShouldProcessNode(context, typedNode);

    protected virtual bool ShouldProcessNode(TContext context, TNode node) => true;

    protected internal sealed override void ProcessNode(TContext context, TBaseNode node) => ProcessNode(context, (TNode) node);

    protected abstract void ProcessNode(TContext context, TNode node);
}