namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A pipeline to process nodes in a tree. A pipeline consists of multiple named stages, each of which has one or more <see cref="Processing.Processor{TNode}" />s
/// running in serial or parallel. Stages can optionally specify whether pipeline processing should continue once the stage has completed.
/// By default, processing will not continue if there are any errors in the tree.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class Pipeline<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    internal Pipeline(IReadOnlyList<PipelineStage<TBaseNode>> stages)
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
    public IReadOnlyList<PipelineStage<TBaseNode>> Stages { get; }

    /// <summary>
    /// Fluent interface to build a <see cref="Pipeline{TNode}"/>.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="PipelineBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The <see cref="Pipeline{TNode}"/>.</returns>
    [Pure]
    public static Pipeline<TBaseNode> Build(Action<PipelineBuilder<TBaseNode>> build)
    {
        var builder = new PipelineBuilder<TBaseNode>();
        build(builder);
        return builder.Build();
    }

    /// <summary>
    /// Runs the pipeline on the specified root node, returning the potentially new root via an out parameter.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TBaseNode}"/>.</param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TBaseNode root, out TBaseNode newRoot)
    {
        var (success, resultRoot, _) = Run(root);
        newRoot = resultRoot;
        return success;
    }

    /// <summary>
    /// Runs the pipeline on the specified root node, returning the potentially new root and last stage run via out parameters.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TBaseNode}"/>.</param>
    /// <param name="lastStageRun">
    /// The name of the last stage that was run. If <c>false</c> is returned then this will be the name of the stage that stopped
    /// further stages from continuing.
    /// </param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TBaseNode root, out TBaseNode newRoot, out string lastStageRun)
    {
        var (success, resultRoot, lastStage) = Run(root);
        newRoot = resultRoot;
        lastStageRun = lastStage;
        return success;
    }

    /// <summary>
    /// Runs the pipeline on the specified root node, returning a tuple with the result, the potentially replaced root node and the last stage run.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns>A tuple of whether all stages ran successfully, the root node which may have been replaced, and the name of the last stage that was run.</returns>
    public (bool Success, TBaseNode Root, string LastStageRun) Run(TBaseNode root)
    {
        var lastStageRun = (string)null!;

        foreach (var stage in Stages)
        {
            lastStageRun = stage.Name;

            var (success, newRoot) = stage.Run(root);
            root = newRoot;

            if (!success)
            {
                return (false, root, lastStageRun);
            }
        }

        return (true, root, lastStageRun);
    }
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
    /// Runs the pipeline on the specified root node, returning the potentially new root via an out parameter.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TContext, TBaseNode}"/>.</param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot)
    {
        var (success, resultRoot, _) = Run(context, root);
        newRoot = resultRoot;
        return success;
    }

    /// <summary>
    /// Runs the pipeline on the specified root node, returning the potentially new root and last stage run via out parameters.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TContext, TBaseNode}"/>.</param>
    /// <param name="lastStageRun">
    /// The name of the last stage that was run. If <c>false</c> is returned then this will be the name of the stage that stopped
    /// further stages from continuing.
    /// </param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot, out string lastStageRun)
    {
        var (success, resultRoot, lastStage) = Run(context, root);
        newRoot = resultRoot;
        lastStageRun = lastStage;
        return success;
    }

    /// <summary>
    /// Runs the pipeline on the specified root node, returning a tuple with the result, the potentially replaced root node and the last stage run.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns>A tuple of whether all stages ran successfully, the root node which may have been replaced, and the name of the last stage that was run.</returns>
    public (bool Success, TBaseNode Root, string LastStageRun) Run(TContext context, TBaseNode root)
    {
        string lastStageRun = null!;

        foreach (var stage in Stages)
        {
            lastStageRun = stage.Name;

            var (success, newRoot) = stage.Run(context, root);
            root = newRoot;

            if (!success)
            {
                return (false, root, lastStageRun);
            }
        }

        return (true, root, lastStageRun);
    }
}
