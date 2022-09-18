using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Processing;

public abstract class Processor<TNode>
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

    [MustUseReturnValue]
    private static TResult CatchAndRethrowExceptions<TResult>(TNode node, string method, Func<TNode, TResult> function)
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
    
    private static void CatchAndRethrowExceptions(TNode node, string method, Action<TNode> action)
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
    
    protected virtual IDescendentEnumerator<TNode> Enumerator => DepthFirstPreOrder<TNode>.Instance;

    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;
    
    [Pure]
    protected virtual bool ShouldProcessChildren(TNode node) => true;

    protected abstract void ProcessNode(TNode node);
}

public abstract class Processor<TNode, TBaseNode> : Processor<TBaseNode>
    where TNode : TBaseNode
    where TBaseNode : Node<TBaseNode>
{
    protected sealed override bool ShouldProcessNode(TBaseNode node) => node is TNode typedNode && ShouldProcessNode(typedNode);

    protected virtual bool ShouldProcessNode(TNode node) => true;

    protected sealed override void ProcessNode(TBaseNode node) => ProcessNode((TNode) node);

    protected abstract void ProcessNode(TNode node);
}