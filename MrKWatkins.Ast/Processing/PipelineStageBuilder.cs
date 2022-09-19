namespace MrKWatkins.Ast.Processing;

// ReSharper disable ParameterHidesMember
public sealed class PipelineStageBuilder<TNode>
    where TNode : Node<TNode>
{
    private readonly List<Processor<TNode>> processors = new();
    private string name;
    private Func<TNode, bool> shouldContinue = root => !root.ThisAndDescendentsHaveErrors;
    private int? maxDegreeOfParallelism;

    internal PipelineStageBuilder(int number)
    {
        name = number.ToString();
    }

    [Pure]
    internal PipelineStage<TNode> Build() => new(
        name,
        maxDegreeOfParallelism.HasValue
            ? new[] { new ParallelProcessor<TNode>(maxDegreeOfParallelism.Value, processors) }
            : processors, shouldContinue);
    
    public PipelineStageBuilder<TNode> Add<TProcessor>()
        where TProcessor : Processor<TNode>, new()
    {
        processors.Add(new TProcessor());
        return this;
    }

    public PipelineStageBuilder<TNode> Add(Processor<TNode> processor, params Processor<TNode>[] others)
    {
        processors.Add(processor);
        processors.AddRange(others);
        return this;
    }

    public PipelineStageBuilder<TNode> WithName(string name)
    {
        this.name = name;
        return this;
    }

    public PipelineStageBuilder<TNode> WithShouldContinue(Func<TNode, bool> shouldContinue)
    {
        this.shouldContinue = shouldContinue;
        return this;
    }

    public PipelineStageBuilder<TNode> WithAlwaysContinue() => WithShouldContinue(_ => true);
    
    public PipelineStageBuilder<TNode> WithParallelProcessing(int? maxDegreeOfParallelism = null)
    {
        this.maxDegreeOfParallelism = maxDegreeOfParallelism ?? Environment.ProcessorCount;
        return this;
    }
}