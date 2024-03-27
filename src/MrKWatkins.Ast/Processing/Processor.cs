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
}