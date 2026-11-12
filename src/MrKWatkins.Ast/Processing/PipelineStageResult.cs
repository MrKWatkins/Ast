namespace MrKWatkins.Ast.Processing;

/// <summary>
/// The result of running a <see cref="PipelineStage{TBaseNode}" /> or <see cref="PipelineStage{TContext, TBaseNode}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
/// <param name="success"><c>true</c> if the pipeline should continue to the next stage, <c>false</c> otherwise.</param>
/// <param name="root">The root node of the tree after processing.</param>
public sealed class PipelineStageResult<TBaseNode>(bool success, TBaseNode root)
    where TBaseNode : Node<TBaseNode>
{

    /// <summary>
    /// <c>true</c> if the pipeline should continue to the next stage, <c>false</c> otherwise.
    /// </summary>
    public bool Success { get; } = success;

    /// <summary>
    /// The root node of the tree after processing. Always use this rather than the root passed in, as a replacer may have replaced it.
    /// </summary>
    public TBaseNode Root { get; } = root;

    /// <summary>
    /// Deconstructs the result.
    /// </summary>
    /// <param name="success"><see cref="Success" />.</param>
    /// <param name="root"><see cref="Root" />.</param>
    public void Deconstruct(out bool success, out TBaseNode root)
    {
        success = Success;
        root = Root;
    }
}