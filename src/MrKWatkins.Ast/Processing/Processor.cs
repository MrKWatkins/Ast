namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A processor to traverse a tree of nodes and perform some processing on some or all of them.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public abstract class Processor<TNode>
    where TNode : Node<TNode>
{
    private protected Processor()
    {
    }

    internal abstract ProcessorState<TNode> CreateState(TNode root);

    /// <summary>
    /// Processes a tree of nodes from the specified root node.
    /// </summary>
    /// <param name="root">The root node.</param>
    /// <exception cref="ProcessingException">
    /// If an exception was thrown unrelated to a specific node. The <see cref="Exception.InnerException" /> will contain the originating exception.
    /// </exception>
    /// <exception cref="ProcessingException{TNode}">
    /// If an exception was thrown related to a specific node. The <see cref="Exception.InnerException" /> will contain the originating exception and
    /// <see cref="ProcessingException{TNode}.Node" /> will contain the node that caused the exception.
    /// </exception>
    /// <exception cref="AggregateException">
    /// If multiple exceptions occurred during parallel processing. The <see cref="AggregateException.InnerExceptions" /> will contain
    /// <see cref="PipelineException">PipelineExceptions</see> wrapping the originating exceptions.
    /// </exception>
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

    /// <summary>
    /// Executes the specified function, catching any exceptions thrown. Exceptions are then wrapped in a <see cref="ProcessingException{TNode}" /> that
    /// gives details of the node and method that caused the problem which is then thrown.
    /// </summary>
    /// <param name="node">The node that caused the problem.</param>
    /// <param name="method">The node that method the problem.</param>
    /// <param name="function">The function to execute.</param>
    /// <typeparam name="TResult">The return type of <paramref name="function"/>.</typeparam>
    /// <returns>The result of executing <paramref name="function"/>.</returns>
    /// <exception cref="ProcessingException{TNode}">
    /// If an exception occurs during <paramref name="function" /> then a <see cref="ProcessingException{TNode}" /> will be thrown with details of the node
    /// and method that caused the problem.
    /// </exception>
    [MustUseReturnValue]
    private protected static TResult CatchAndRethrowExceptions<TResult>(TNode node, string method, Func<TNode, TResult> function)
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