using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Processing;

public abstract class Processor
{
    private protected Processor()
    {
    }

    [MustUseReturnValue]
    protected static TResult CatchAndRethrowExceptions<TNode, TResult>(TNode node, string method, Func<TNode, TResult> function)
        where TNode : Node<TNode>
    {
        try
        {
            return function(node);
        }
        catch (Exception exception)
        {
            throw new ProcessingException<TNode>($"Exception during {method}.", exception, node);
        }
    }
    
    protected static void CatchAndRethrowExceptions<TNode>(TNode node, string method, Action<TNode> action)
        where TNode : Node<TNode>
    {
        try
        {
            action(node);
        }
        catch (Exception exception)
        {
            throw new ProcessingException<TNode>($"Exception during {method}.", exception, node);
        }
    }
}

public abstract class Processor<TNode> : Processor, IProcessor<TNode>
    where TNode : Node<TNode>
{
    public void Process(TNode root)
    {
        foreach (var node in Enumerator.Enumerate(root, true, n => CatchAndRethrowExceptions(n, nameof(ShouldProcessChildren), ShouldProcessChildren)))
        {
            if (CatchAndRethrowExceptions(node, nameof(ShouldProcessNode), ShouldProcessNode))
            {
                CatchAndRethrowExceptions(node, nameof(ProcessNode), ProcessNode);
            }
        }
    }
    
    protected internal virtual IDescendentEnumerator<TNode> Enumerator => DepthFirstPreOrder<TNode>.Instance;

    [Pure]
    protected internal virtual bool ShouldProcessNode(TNode node) => true;
    
    [Pure]
    protected internal virtual bool ShouldProcessChildren(TNode node) => true;

    protected internal abstract void ProcessNode(TNode node);
}

public abstract class Processor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    protected internal sealed override bool ShouldProcessNode(TBaseNode node) => node is TNode typedNode && ShouldProcessNode(typedNode);

    protected virtual bool ShouldProcessNode(TNode node) => true;

    protected internal sealed override void ProcessNode(TBaseNode node) => ProcessNode((TNode) node);

    protected abstract void ProcessNode(TNode node);
}