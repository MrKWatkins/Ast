namespace MrKWatkins.Ast.Processing;

public abstract class Processor<TNode>
    where TNode : Node<TNode>
{
    private protected Processor()
    {
    }

    internal abstract ProcessorState<TNode> CreateState(TNode root);

    public void Process(TNode root)
    {
        using var state = CreateState(root);
        
        foreach (var node in EnumerateNodes(state, root))
        {
            state.ProcessNodeIfShould(node);
        }
        
        state.OnComplete?.Invoke(state);

        state.Exceptions.ThrowIfContainsExceptions("One or more exceptions occurred during processing.");
    }

    [Pure]
    private protected virtual IEnumerable<TNode> EnumerateNodes(ProcessorState<TNode> state, TNode root) => Node<TNode>.Traverse.DepthFirstPreOrder(root);
    
    [MustUseReturnValue]
    protected static TResult CatchAndRethrowExceptions<TResult>(TNode node, string method, Func<TNode, TResult> function)
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
}