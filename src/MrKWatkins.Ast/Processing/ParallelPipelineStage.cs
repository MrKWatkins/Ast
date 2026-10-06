using System.Runtime.ExceptionServices;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A <see cref="PipelineStage{TBaseNode}"/> that runs processors in parallel.
/// </summary>
/// <remarks>
/// Processors in a parallel stage must not change the structure of the tree: the tree is walked lazily whilst the processors run, and a node may be
/// processed at the same time as its ancestors and descendents. A processor that returns a node other than the one it was given causes a
/// <see cref="PipelineException" />. See <see cref="ParallelStrategy" /> for how the work is divided between threads.
/// </remarks>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStage<TBaseNode> : PipelineStage<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly ParallelPipelineStage<NoContext, TBaseNode> inner;

    internal ParallelPipelineStage(string name, Func<TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TBaseNode>> processors, int maxDegreeOfParallelism, ParallelStrategy strategy)
        : this(new ParallelPipelineStage<NoContext, TBaseNode>(name, (_, root) => shouldContinue(root), defaultTraversal, processors, maxDegreeOfParallelism, strategy), shouldContinue, processors)
    {
    }

    private ParallelPipelineStage(ParallelPipelineStage<NoContext, TBaseNode> inner, Func<TBaseNode, bool> shouldContinue, IReadOnlyList<Processor<TBaseNode>> processors)
        : base(inner, shouldContinue)
    {
        this.inner = inner;
        Processors = processors;
    }

    /// <summary>
    /// The processors in the stage.
    /// </summary>
    /// <returns>The processors.</returns>
    public IReadOnlyList<Processor<TBaseNode>> Processors { get; }

    /// <summary>
    /// The maximum degree of parallelism to use when processing nodes.
    /// </summary>
    /// <returns>The maximum degree of parallelism.</returns>
    public int MaxDegreeOfParallelism => inner.MaxDegreeOfParallelism;

    /// <summary>
    /// How the stage divides its work between threads.
    /// </summary>
    /// <returns>The strategy.</returns>
    public ParallelStrategy Strategy => inner.Strategy;
}

/// <summary>
/// A <see cref="PipelineStage{TContext, TBaseNode}"/> that runs processors in parallel.
/// </summary>
/// <remarks>
/// Processors in a parallel stage must not change the structure of the tree: the tree is walked lazily whilst the processors run, and a node may be
/// processed at the same time as its ancestors and descendents. A processor that returns a node other than the one it was given causes a
/// <see cref="PipelineException" />. See <see cref="ParallelStrategy" /> for how the work is divided between threads.
/// </remarks>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStage<TContext, TBaseNode> : PipelineStage<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal ParallelPipelineStage(string name, Func<TContext, TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal, IReadOnlyList<Processor<TContext, TBaseNode>> processors, int maxDegreeOfParallelism, ParallelStrategy strategy)
        : base(name, shouldContinue, defaultTraversal)
    {
        if (processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }

        if (processors.Any(p => p is IOrderedProcessor<TContext, TBaseNode>))
        {
            throw new ArgumentException("OrderedProcessors cannot be used in a parallel stage.", nameof(processors));
        }

        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        if (!Enum.IsDefined(strategy))
        {
            throw new ArgumentOutOfRangeException(nameof(strategy), strategy, "Value is not a valid strategy.");
        }

        Processors = processors;
        MaxDegreeOfParallelism = maxDegreeOfParallelism;
        Strategy = strategy;
    }

    /// <summary>
    /// The processors in the stage.
    /// </summary>
    /// <returns>The processors.</returns>
    public IReadOnlyList<Processor<TContext, TBaseNode>> Processors { get; }

    /// <summary>
    /// The maximum degree of parallelism to use when processing nodes.
    /// </summary>
    /// <returns>The maximum degree of parallelism.</returns>
    public int MaxDegreeOfParallelism { get; }

    /// <summary>
    /// How the stage divides its work between threads.
    /// </summary>
    /// <returns>The strategy.</returns>
    public ParallelStrategy Strategy { get; }

    private protected override TBaseNode Process(TContext context, TBaseNode root)
    {
        var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };

        try
        {
            switch (Strategy)
            {
                case ParallelStrategy.PerNode:
                    Parallel.ForEach(
                        DefaultTraversal.Enumerate(root),
                        options,
                        node =>
                        {
                            foreach (var processor in Processors)
                            {
                                Process(context, processor, node);
                            }
                        });
                    break;

                case ParallelStrategy.PerProcessor:
                    Parallel.ForEach(
                        Processors,
                        options,
                        processor =>
                        {
                            foreach (var node in DefaultTraversal.Enumerate(root))
                            {
                                Process(context, processor, node);
                            }
                        });
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported strategy {Strategy}.");
            }
        }
        catch (AggregateException exception)
        {
            // Parallel.ForEach wraps exceptions in an AggregateException; unwrap the first PipelineException so parallel stages throw the same as serial ones.
            var pipelineException = exception.InnerExceptions.OfType<PipelineException>().FirstOrDefault();
            if (pipelineException != null)
            {
                ExceptionDispatchInfo.Throw(pipelineException);
            }

            throw;
        }

        return root;
    }

    private void Process(TContext context, Processor<TContext, TBaseNode> processor, TBaseNode node)
    {
        TBaseNode result;
        try
        {
            result = processor.Process(context, node);
        }
        catch (Exception exception)
        {
            throw new PipelineException($"Exception occurred executing processor {processor.GetType().SimpleName()} for node {node}.", Name, exception);
        }

        if (!ReferenceEquals(result, node))
        {
            throw new PipelineException($"Processor {processor.GetType().SimpleName()} returned a different node for node {node}. Processors in a parallel stage cannot replace nodes.", Name);
        }
    }
}