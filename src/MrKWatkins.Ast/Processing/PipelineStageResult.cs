namespace MrKWatkins.Ast.Processing;

/// <summary>
/// The result of running a <see cref="PipelineStage{TBaseNode}" /> or <see cref="PipelineStage{TContext, TBaseNode}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class PipelineStageResult<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="PipelineStageResult{TBaseNode}" /> class.
    /// </summary>
    /// <param name="success"><c>true</c> if the pipeline should continue to the next stage, <c>false</c> otherwise.</param>
    /// <param name="root">The root node of the tree after processing.</param>
    public PipelineStageResult(bool success, TBaseNode root)
    {
        Success = success;
        Root = root;
    }

    /// <summary>
    /// <c>true</c> if the pipeline should continue to the next stage, <c>false</c> otherwise.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// The root node of the tree after processing. Always use this rather than the root passed in, as a replacer may have replaced it.
    /// </summary>
    public TBaseNode Root { get; }

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