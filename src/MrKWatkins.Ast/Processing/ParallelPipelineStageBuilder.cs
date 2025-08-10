namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a parallel pipeline stage.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStageBuilder<TBaseNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TBaseNode>, ParallelPipelineStage<TBaseNode>, TBaseNode, Processor<TBaseNode>, Func<TBaseNode, bool>>
    where TBaseNode : Node<TBaseNode>
{
    private int maxDegreeOfParallelism = Environment.ProcessorCount;

    internal ParallelPipelineStageBuilder(int number)
        : base(number, root => !root.ThisAndDescendentsHaveErrors)
    {
    }

    [Pure]
    internal override ParallelPipelineStage<TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors, maxDegreeOfParallelism);

    private protected override void VerifyProcessorCanBeAdded(Processor<TBaseNode> processor)
    {
        if (processor is OrderedProcessor<TBaseNode>)
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

    internal ParallelPipelineStageBuilder(int number)
        : base(number, (_, root) => !root.ThisAndDescendentsHaveErrors)
    {
    }

    [Pure]
    internal override ParallelPipelineStage<TContext, TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors, maxDegreeOfParallelism);

    private protected override void VerifyProcessorCanBeAdded(Processor<TContext, TBaseNode> processor)
    {
        if (processor is OrderedProcessor<TContext, TBaseNode>)
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
}