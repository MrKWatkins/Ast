namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A pipeline to process nodes in a tree. A pipeline consists of multiple named stages, each of which has one or more <see cref="Processor{TNode}" />s
/// running in serial or parallel. Stages can optionally have specify whether pipeline processing should continue once the stage has completed.
/// By default processing will not continue if there are any errors in the tree.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class Pipeline<TNode>
    where TNode : Node<TNode>
{
    internal Pipeline(IReadOnlyList<PipelineStage<TNode>> stages)
    {
        if (stages.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(stages));
        }
        Stages = stages;
    }

    internal IReadOnlyList<PipelineStage<TNode>> Stages { get; }

    /// <summary>
    /// Fluent interface to build a <see cref="Pipeline{TNode}"/>.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="PipelineBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The <see cref="Pipeline{TNode}"/>.</returns>
    [Pure]
    public static Pipeline<TNode> Build(Action<PipelineBuilder<TNode>> build)
    {
        var builder = new PipelineBuilder<TNode>();
        build(builder);
        return builder.Build();
    }

    /// <summary>
    /// Runs the pipeline on the specified root node.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TNode root) => Run(root, out _);

    /// <summary>
    /// Runs the pipeline on the specified root node.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <param name="lastStageRan">
    /// The name of the last stage that was ran. If <c>false</c> is returned then this will be the name of the stage that stopped
    /// further stages from continuing.
    /// </param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TNode root, out string lastStageRan)
    {
        lastStageRan = null!;

        foreach (var stage in Stages)
        {
            lastStageRan = stage.Name;

            if (!stage.Run(root))
            {
                return false;
            }
        }

        return true;
    }
}