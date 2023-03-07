// ReSharper disable CoVariantArrayConversion
namespace MrKWatkins.Ast.Processing;

// ReSharper disable ParameterHidesMember
public sealed class PipelineBuilder<TNode>
    where TNode : Node<TNode>
{
    private readonly List<PipelineStage<TNode>> stages = new();

    [Pure]
    internal Pipeline<TNode> Build() => new(stages);
    
    public PipelineBuilder<TNode> AddStage(Action<SerialPipelineStageBuilder<TNode>> build)
    {
        var builder = new SerialPipelineStageBuilder<TNode>(stages.Count + 1);
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
    
    public PipelineBuilder<TNode> AddStage(Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.Add(processor, others));
    
    public PipelineBuilder<TNode> AddStage(string name, Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.WithName(name).Add(processor, others));
    
    public PipelineBuilder<TNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TNode>> build)
    {
        var builder = new ParallelPipelineStageBuilder<TNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    public PipelineBuilder<TNode> AddParallelStage(UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.Add(processor1).Add(processor2, others));
    
    public PipelineBuilder<TNode> AddParallelStage(string name, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.WithName(name).Add(processor1).Add(processor2, others));
    
    public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.Add(processor1).Add(processor2, others).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));
    
    public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, string name, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.WithName(name).Add(processor1).Add(processor2, others).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));
}