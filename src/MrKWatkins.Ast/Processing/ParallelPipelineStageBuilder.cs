namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a parallel pipeline stage.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class ParallelPipelineStageBuilder<TNode> : PipelineStageBuilder<ParallelPipelineStageBuilder<TNode>, UnorderedProcessor<TNode>, TNode>
    where TNode : Node<TNode>
{
    private int? maxDegreeOfParallelism;

    internal ParallelPipelineStageBuilder(int number)
        : base(number)
    {
    }

    [Pure]
    internal override PipelineStage<TNode> Build() => new(Name, new[] { new ParallelProcessor<TNode>(Processors, maxDegreeOfParallelism) }, ShouldContinue);

    /// <summary>
    /// Sets the maximum degree of parallelism for parallel processing. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread
    /// will be used to walk the tree and the other threads will be used to process the nodes. Defaults to the number of processors in the machine.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxDegreeOfParallelism"/> is less than 1.</exception>
    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        this.maxDegreeOfParallelism = maxDegreeOfParallelism;

        return Self;
    }
}