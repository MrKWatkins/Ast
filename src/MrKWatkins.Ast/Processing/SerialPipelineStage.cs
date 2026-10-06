using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="PipelineStage{TBaseNode}"/> that runs processors one after the other.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class SerialPipelineStage<TBaseNode> : PipelineStage<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal SerialPipelineStage(string name, Func<TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TBaseNode>> processors)
        : base(new SerialPipelineStage<NoContext, TBaseNode>(name, (_, root) => shouldContinue(root), defaultTraversal, processors), shouldContinue)
    {
        Processors = processors;
    }

    /// <summary>
    /// The processors in this stage.
    /// </summary>
    public IReadOnlyList<Processor<TBaseNode>> Processors { get; }
}

/// <summary>
/// A <see cref="PipelineStage{TBaseNode}"/> that runs processors one after the other.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class SerialPipelineStage<TContext, TBaseNode> : PipelineStage<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal SerialPipelineStage(string name, Func<TContext, TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TContext, TBaseNode>> processors)
        : base(name, shouldContinue, defaultTraversal)
    {
        if (processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }

        Processors = processors;
    }

    /// <summary>
    /// The processors in this stage.
    /// </summary>
    public IReadOnlyList<Processor<TContext, TBaseNode>> Processors { get; }

    /// <inheritdoc />
    private protected override TBaseNode Process(TContext context, TBaseNode root)
    {
        foreach (var processor in Processors)
        {
            try
            {
                root = processor is IOrderedProcessor<TContext, TBaseNode> orderedProcessor
                    ? Process(context, root, processor, orderedProcessor)
                    : Process(context, root, processor);
            }
            catch (Exception exception) when (exception is not PipelineException)
            {
                // Exceptions from processing a node will already be wrapped; this catches exceptions from traversing the tree.
                throw new PipelineException($"Exception occurred executing processor {processor.GetType().SimpleName()}.", Name, exception);
            }
        }

        return root;
    }

    private TBaseNode Process(TContext context, TBaseNode root, Processor<TContext, TBaseNode> processor)
    {
        foreach (var node in DefaultTraversal.Enumerate(root))
        {
            root = Process(context, root, processor, node);
        }

        return root;
    }

    private TBaseNode Process(TContext context, TBaseNode root, Processor<TContext, TBaseNode> processor, IOrderedProcessor<TContext, TBaseNode> orderedProcessor)
    {
        var traversal = orderedProcessor.GetTraversal(context, root);

        foreach (var node in traversal.Enumerate(root, shouldEnumerateDescendants: n => orderedProcessor.ShouldProcessDescendants(context, n)))
        {
            root = Process(context, root, processor, node);
        }

        return root;
    }

    private TBaseNode Process(TContext context, TBaseNode root, Processor<TContext, TBaseNode> processor, TBaseNode node)
    {
        TBaseNode? newRoot;
        try
        {
            newRoot = processor.ProcessInPipeline(context, node);
        }
        catch (Exception exception)
        {
            throw new PipelineException($"Exception occurred executing processor {processor.GetType().SimpleName()} for node {node}.", Name, exception);
        }

        return newRoot ?? root;
    }
}