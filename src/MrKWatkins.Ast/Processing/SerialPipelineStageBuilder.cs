namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a serial pipeline stage.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class SerialPipelineStageBuilder<TBaseNode> : PipelineStageBuilder<SerialPipelineStageBuilder<TBaseNode>, SerialPipelineStage<TBaseNode>, TBaseNode, Processor<TBaseNode>, Func<TBaseNode, bool>>
    where TBaseNode : Node<TBaseNode>
{
    internal SerialPipelineStageBuilder(int number)
        : base(number, root => !root.ThisAndDescendentsHaveErrors)
    {
    }

    [Pure]
    internal override SerialPipelineStage<TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors);

    /// <summary>
    /// Always continue to the next stage after this one, irrespective of errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public override SerialPipelineStageBuilder<TBaseNode> WithAlwaysContinue() => WithShouldContinue(_ => true);
}

/// <summary>
/// Fluent builder for a serial pipeline stage.
/// </summary>
/// <typeparam name="TContext">The type of the context for the processing.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class SerialPipelineStageBuilder<TContext, TBaseNode> : PipelineStageBuilder<SerialPipelineStageBuilder<TContext, TBaseNode>, SerialPipelineStage<TContext, TBaseNode>, TBaseNode, Processor<TContext, TBaseNode>, Func<TContext, TBaseNode, bool>>
    where TBaseNode : Node<TBaseNode>
{
    internal SerialPipelineStageBuilder(int number)
        : base(number, (_, root) => !root.ThisAndDescendentsHaveErrors)
    {
    }

    [Pure]
    internal override SerialPipelineStage<TContext, TBaseNode> Build() => new(Name, ShouldContinue, DefaultTraversal, Processors);

    /// <summary>
    /// Always continue to the next stage after this one, irrespective of errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public override SerialPipelineStageBuilder<TContext, TBaseNode> WithAlwaysContinue() => WithShouldContinue((_, _) => true);
}