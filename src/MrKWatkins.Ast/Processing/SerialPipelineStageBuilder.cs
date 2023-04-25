namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a serial pipeline stage.
/// </summary>
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