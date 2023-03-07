namespace MrKWatkins.Ast.Processing;

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
    
    [Pure]
    public static Pipeline<TNode> Build(Action<PipelineBuilder<TNode>> build)
    {
        var builder = new PipelineBuilder<TNode>();
        build(builder);
        return builder.Build();
    }
    
    /// <summary>
    /// Runs the pipeline on the specified <param name="root" /> node.
    /// </summary>
    /// <param name="root">The root node to run the pipeline on.</param>
    /// <returns><c>true</c> if all stages ran successfully, <c>false</c> otherwise.</returns>
    public bool Run(TNode root) => Run(root, out _);

    /// <summary>
    /// Runs the pipeline on the specified <param name="root" />.
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