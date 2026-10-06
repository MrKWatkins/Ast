namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A pipeline to process nodes in a tree. A pipeline consists of multiple named stages, each of which has one or more <see cref="Processor{TBaseNode}" />s
/// running in serial or parallel. Stages can optionally specify whether pipeline processing should continue once the stage has completed.
/// By default, processing will not continue if there are any errors in the tree.
/// </summary>
/// <remarks>
/// A pipeline that does not take a context is a thin wrapper over a <see cref="Pipeline{TContext, TBaseNode}" /> using <see cref="NoContext" />.
/// </remarks>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class Pipeline<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly Pipeline<NoContext, TBaseNode> inner;

    internal Pipeline(IReadOnlyList<PipelineStage<TBaseNode>> stages)
    {
        inner = new Pipeline<NoContext, TBaseNode>(stages.Select(s => s.Inner).ToList());
        Stages = stages;
    }

    /// <summary>
    /// The stages in the pipeline.
    /// </summary>
    public IReadOnlyList<PipelineStage<TBaseNode>> Stages { get; }

    /// <summary>
    /// Fluent interface to build a <see cref="Pipeline{TBaseNode}"/>.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="PipelineBuilder{TBaseNode}"/> to build the pipeline.</param>
    /// <returns>The pipeline.</returns>
    [Pure]
    public static Pipeline<TBaseNode> Build(Action<PipelineBuilder<TBaseNode>> build)
    {
        var builder = new PipelineBuilder<TBaseNode>();
        build(builder);
        return builder.Build();
    }

    /// <summary>
    /// Runs the pipeline on the specified root node.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns>The result of running the pipeline, including the root node after processing, which may have been replaced.</returns>
    public PipelineResult<TBaseNode> Run(TBaseNode root) => inner.Run(default, root);
}

/// <summary>
/// A pipeline to process nodes in a tree with a context. A pipeline consists of multiple named stages, each of which has one or more
/// <see cref="Processor{TContext, TNode}" />s running in serial or parallel. Stages can optionally specify whether pipeline processing
/// should continue once the stage has completed. By default, processing will not continue if there are any errors in the tree.
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class Pipeline<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal Pipeline(IReadOnlyList<PipelineStage<TContext, TBaseNode>> stages)
    {
        if (stages.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(stages));
        }

        Stages = stages;
    }

    /// <summary>
    /// The stages in the pipeline.
    /// </summary>
    public IReadOnlyList<PipelineStage<TContext, TBaseNode>> Stages { get; }

    /// <summary>
    /// Fluent interface to build a <see cref="Pipeline{TContext, TNode}"/>.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="PipelineBuilder{TContext, TNode}"/> to build the pipeline.</param>
    /// <returns>The <see cref="Pipeline{TContext, TNode}"/>.</returns>
    [Pure]
    public static Pipeline<TContext, TBaseNode> Build(Action<PipelineBuilder<TContext, TBaseNode>> build)
    {
        var builder = new PipelineBuilder<TContext, TBaseNode>();
        build(builder);
        return builder.Build();
    }

    /// <summary>
    /// Runs the pipeline on the specified root node.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns>The result of running the pipeline, including the root node after processing, which may have been replaced.</returns>
    public PipelineResult<TBaseNode> Run(TContext context, TBaseNode root)
    {
        string lastStageRun = null!;

        foreach (var stage in Stages)
        {
            lastStageRun = stage.Name;

            var result = stage.Run(context, root);
            root = result.Root;

            if (!result.Success)
            {
                return new PipelineResult<TBaseNode>(false, root, lastStageRun);
            }
        }

        return new PipelineResult<TBaseNode>(true, root, lastStageRun);
    }
}