namespace MrKWatkins.Ast.Processing;

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

    // ReSharper disable once ParameterHidesMember
    public ParallelPipelineStageBuilder<TNode> WithMaxDegreeOfParallelism(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        this.maxDegreeOfParallelism = maxDegreeOfParallelism;

        return This;
    }
}