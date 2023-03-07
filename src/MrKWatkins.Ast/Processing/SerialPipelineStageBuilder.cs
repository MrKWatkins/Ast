namespace MrKWatkins.Ast.Processing;

public sealed class SerialPipelineStageBuilder<TNode> : PipelineStageBuilder<SerialPipelineStageBuilder<TNode>, Processor<TNode>, TNode>
    where TNode : Node<TNode>
{
    internal SerialPipelineStageBuilder(int number)
        : base(number)
    {
    }

    [Pure]
    internal override PipelineStage<TNode> Build() => new(Name, Processors, ShouldContinue);
}