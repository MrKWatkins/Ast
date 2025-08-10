using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="PipelineStage{TBaseNode}"/> that runs processors in parallel.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStage<TBaseNode> : PipelineStage<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal ParallelPipelineStage(string name, Func<TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TBaseNode>> processors, int maxDegreeOfParallelism)
        : base(name, shouldContinue, defaultTraversal)
    {
        if (processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }

        if (processors.Any(p => p is OrderedProcessor<TBaseNode>))
        {
            throw new ArgumentException("OrderedProcessors cannot be used in a parallel stage.", nameof(processors));
        }

        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        Processors = processors;
        MaxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    /// <summary>
    /// The processors in the stage.
    /// </summary>
    public IReadOnlyList<Processor<TBaseNode>> Processors { get; }

    /// <summary>
    /// The maximum degree of parallelism to use when processing nodes.
    /// </summary>
    public int MaxDegreeOfParallelism { get; }

    /// <inheritdoc />
    private protected override void Process(TBaseNode root)
    {
        var nodesWithProcessors = DefaultTraversal.Enumerate(root).SelectMany(node => Processors.Select(processor => (Node: node, Processor: processor)));

        Parallel.ForEach(
            nodesWithProcessors,
            new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism },
            nodeWithProcessor =>
            {
                try
                {
                    nodeWithProcessor.Processor.Process(nodeWithProcessor.Node);
                }
                catch (Exception exception)
                {
                    throw new PipelineException($"Exception occurred executing processor {nodeWithProcessor.Processor.GetType().SimpleName()} for node {nodeWithProcessor.Node}.", Name, exception);
                }
            });
    }
}

/// <summary>
/// A <see cref="PipelineStage{TContext, TBaseNode}"/> that runs processors in parallel.
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStage<TContext, TBaseNode> : PipelineStage<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal ParallelPipelineStage(string name, Func<TContext, TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TContext, TBaseNode>> processors, int maxDegreeOfParallelism)
        : base(name, shouldContinue, defaultTraversal)
    {
        if (processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }

        if (processors.Any(p => p is OrderedProcessor<TContext, TBaseNode>))
        {
            throw new ArgumentException("OrderedProcessors cannot be used in a parallel stage.", nameof(processors));
        }

        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        Processors = processors;
        MaxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    /// <summary>
    /// The processors in the stage.
    /// </summary>
    public IReadOnlyList<Processor<TContext, TBaseNode>> Processors { get; }

    /// <summary>
    /// The maximum degree of parallelism to use when processing nodes.
    /// </summary>
    public int MaxDegreeOfParallelism { get; }

    /// <inheritdoc />
    private protected override void Process(TContext context, TBaseNode root)
    {
        var nodesWithProcessors = DefaultTraversal.Enumerate(root).SelectMany(node => Processors.Select(processor => (Node: node, Processor: processor)));

        Parallel.ForEach(
            nodesWithProcessors,
            new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism },
            nodeWithProcessor =>
            {
                try
                {
                    nodeWithProcessor.Processor.Process(context, nodeWithProcessor.Node);
                }
                catch (Exception exception)
                {
                    throw new PipelineException($"Exception occurred executing processor {nodeWithProcessor.Processor.GetType().SimpleName()} for node {nodeWithProcessor.Node}.", Name, exception);
                }
            });
    }
}