// ReSharper disable CoVariantArrayConversion
namespace MrKWatkins.Ast.Processing;

// ReSharper disable ParameterHidesMember
public sealed class PipelineBuilder<TNode>
    where TNode : Node<TNode>
{
    private readonly List<PipelineStage<TNode>> stages = new();

    [Pure]
    internal Pipeline<TNode> Build() => new(stages);
    
    public PipelineBuilder<TNode> AddStage(Action<PipelineStageBuilder<TNode>> build)
    {
        var builder = new PipelineStageBuilder<TNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    public PipelineBuilder<TNode> AddStage<TProcessor>()
        where TProcessor : Processor<TNode>, new() =>
        AddStage(b => b.Add<TProcessor>());
    
    public PipelineBuilder<TNode> AddStage<TProcessor>(string name)
        where TProcessor : Processor<TNode>, new() =>
        AddStage(b => b.WithName(name).Add<TProcessor>());
    
    public PipelineBuilder<TNode> AddStage(IProcessor<TNode> processor, params IProcessor<TNode>[] others) =>
        AddStage(b => b.Add(processor, others));
    
    public PipelineBuilder<TNode> AddStage(string name, IProcessor<TNode> processor, params IProcessor<TNode>[] others) =>
        AddStage(b => b.WithName(name).Add(processor, others));
    
    public PipelineBuilder<TNode> AddParallelStage(IProcessor<TNode> processor, params IProcessor<TNode>[] others) =>
        AddStage(b => b.Add(processor, others).WithParallelProcessing());
    
    public PipelineBuilder<TNode> AddParallelStage(string name, Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.WithName(name).Add(processor, others).WithParallelProcessing());
    
    public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.Add(processor, others).WithParallelProcessing(maxDegreeOfParallelism));
    
    public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, string name, Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.WithName(name).Add(processor, others).WithParallelProcessing(maxDegreeOfParallelism));
}