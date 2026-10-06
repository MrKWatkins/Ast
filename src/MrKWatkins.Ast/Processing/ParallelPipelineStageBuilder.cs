namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a parallel pipeline stage.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStageBuilder<TBaseNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TBaseNode>, ParallelPipelineStage<TBaseNode>, TBaseNode, Processor<TBaseNode>, Func<TBaseNode, bool>>
    where TBaseNode : Node<TBaseNode>
{
    private int maxDegreeOfParallelism = Environment.ProcessorCount;
    private ParallelStrategy strategy = ParallelStrategy.PerNode;

    internal ParallelPipelineStageBuilder(int number)
        : base(number, root => !root.ThisAndDescendantsHaveErrors)
    {
    }

    [Pure]
    internal override ParallelPipelineStage<TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors, maxDegreeOfParallelism, strategy);

    private protected override void VerifyProcessorCanBeAdded(Processor<TBaseNode> processor)
    {
        if (processor is IOrderedProcessor<NoContext, TBaseNode>)
        {
            throw new ArgumentException("A parallel stage cannot contain an ordered processor.", nameof(processor));
        }
    }

    /// <summary>
    /// Always continue to the next stage after this one, irrespective of errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public override ParallelPipelineStageBuilder<TBaseNode> WithAlwaysContinue() => WithShouldContinue(_ => true);

    /// <summary>
    /// Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxDegreeOfParallelism"/> is less than 1.</exception>
    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TBaseNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        this.maxDegreeOfParallelism = maxDegreeOfParallelism;

        return Self;
    }

    /// <summary>
    /// Sets how the stage divides its work between threads. Defaults to <see cref="ParallelStrategy.PerNode" />.
    /// </summary>
    /// <param name="strategy">The strategy.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="strategy" /> is not a valid <see cref="ParallelStrategy" />.</exception>
    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TBaseNode> WithStrategy(ParallelStrategy strategy)
    {
        if (!Enum.IsDefined(strategy))
        {
            throw new ArgumentOutOfRangeException(nameof(strategy), strategy, "Value is not a valid strategy.");
        }

        this.strategy = strategy;

        return Self;
    }
}

/// <summary>
/// Fluent builder for a parallel pipeline stage.
/// </summary>
/// <typeparam name="TContext">The type of the context for the processing.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStageBuilder<TContext, TBaseNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TContext, TBaseNode>, ParallelPipelineStage<TContext, TBaseNode>, TBaseNode, Processor<TContext, TBaseNode>, Func<TContext, TBaseNode, bool>>
    where TBaseNode : Node<TBaseNode>
{
    private int maxDegreeOfParallelism = Environment.ProcessorCount;
    private ParallelStrategy strategy = ParallelStrategy.PerNode;

    internal ParallelPipelineStageBuilder(int number)
        : base(number, (_, root) => !root.ThisAndDescendantsHaveErrors)
    {
    }

    [Pure]
    internal override ParallelPipelineStage<TContext, TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors, maxDegreeOfParallelism, strategy);

    private protected override void VerifyProcessorCanBeAdded(Processor<TContext, TBaseNode> processor)
    {
        if (processor is IOrderedProcessor<TContext, TBaseNode>)
        {
            throw new ArgumentException("A parallel stage cannot contain an ordered processor.", nameof(processor));
        }
    }

    /// <summary>
    /// Always continue to the next stage after this one, irrespective of errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public override ParallelPipelineStageBuilder<TContext, TBaseNode> WithAlwaysContinue() => WithShouldContinue((_, _) => true);

    /// <summary>
    /// Sets the maximum degree of parallelism for parallel processing. Defaults to the number of processors in the machine.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxDegreeOfParallelism"/> is less than 1.</exception>
    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TContext, TBaseNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        this.maxDegreeOfParallelism = maxDegreeOfParallelism;

        return Self;
    }

    /// <summary>
    /// Sets how the stage divides its work between threads. Defaults to <see cref="ParallelStrategy.PerNode" />.
    /// </summary>
    /// <param name="strategy">The strategy.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="strategy" /> is not a valid <see cref="ParallelStrategy" />.</exception>
    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TContext, TBaseNode> WithStrategy(ParallelStrategy strategy)
    {
        if (!Enum.IsDefined(strategy))
        {
            throw new ArgumentOutOfRangeException(nameof(strategy), strategy, "Value is not a valid strategy.");
        }

        this.strategy = strategy;

        return Self;
    }
}