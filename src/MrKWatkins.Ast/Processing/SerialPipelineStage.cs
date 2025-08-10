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
    public IReadOnlyList<Processor<TBaseNode>> Processors { get; }

    /// <inheritdoc />
    private protected override void Process(TBaseNode root)
    {
        foreach (var processor in Processors)
        {
            try
            {
                if (processor is OrderedProcessor<TBaseNode> orderedProcessor)
                {
                    Process(root, orderedProcessor);
                }
                else
                {
                    Process(root, processor);
                }
            }
            catch (Exception exception)
            {
                throw new PipelineException($"Exception occurred executing processor {processor.GetType().SimpleName()}.", Name, exception);
            }
        }
    }

    private void Process(TBaseNode root, Processor<TBaseNode> processor)
    {
        foreach (var node in DefaultTraversal.Enumerate(root))
        {
            processor.Process(node);
        }
    }

    private static void Process(TBaseNode root, OrderedProcessor<TBaseNode> processor)
    {
        var traversal = processor.GetTraversal(root);

        foreach (var node in traversal.Enumerate(root, shouldEnumerateDescendents: processor.ShouldProcessDescendents))
        {
            processor.Process(node);
        }
    }
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
    private protected override void Process(TContext context, TBaseNode root)
    {
        foreach (var processor in Processors)
        {
            try
            {
                if (processor is OrderedProcessor<TContext, TBaseNode> orderedProcessor)
                {
                    Process(context, root, orderedProcessor);
                }
                else
                {
                    Process(context, root, processor);
                }
            }
            catch (Exception exception)
            {
                throw new PipelineException($"Exception occurred executing processor {processor.GetType().SimpleName()}.", Name, exception);
            }
        }
    }

    private void Process(TContext context, TBaseNode root, Processor<TContext, TBaseNode> processor)
    {
        foreach (var node in DefaultTraversal.Enumerate(root))
        {
            processor.Process(context, node);
        }
    }

    private static void Process(TContext context, TBaseNode root, OrderedProcessor<TContext, TBaseNode> processor)
    {
        var traversal = processor.GetTraversal(context, root);

        foreach (var node in traversal.Enumerate(root, shouldEnumerateDescendents: n => processor.ShouldProcessDescendents(context, n)))
        {
            processor.Process(context, node);
        }
    }
}