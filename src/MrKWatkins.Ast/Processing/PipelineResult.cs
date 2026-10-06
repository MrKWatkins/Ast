namespace MrKWatkins.Ast.Processing;

/// <summary>
/// The result of running a <see cref="Pipeline{TBaseNode}" /> or <see cref="Pipeline{TContext, TBaseNode}" />.
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public sealed class PipelineResult<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="PipelineResult{TBaseNode}" /> class.
    /// </summary>
    /// <param name="success"><c>true</c> if all stages in the pipeline ran, <c>false</c> if a stage stopped the pipeline.</param>
    /// <param name="root">The root node of the tree after processing.</param>
    /// <param name="lastStageRun">The name of the last stage that ran.</param>
    public PipelineResult(bool success, TBaseNode root, string lastStageRun)
    {
        Success = success;
        Root = root;
        LastStageRun = lastStageRun;
    }

    /// <summary>
    /// <c>true</c> if all stages in the pipeline ran, <c>false</c> if a stage stopped the pipeline.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// The root node of the tree after processing. Always use this rather than the root passed in, as a replacer may have replaced it.
    /// </summary>
    public TBaseNode Root { get; }

    /// <summary>
    /// The name of the last stage that ran. If <see cref="Success" /> is <c>false</c> this is the stage that stopped the pipeline.
    /// </summary>
    public string LastStageRun { get; }

    /// <summary>
    /// Deconstructs the result.
    /// </summary>
    /// <param name="success"><see cref="Success" />.</param>
    /// <param name="root"><see cref="Root" />.</param>
    /// <param name="lastStageRun"><see cref="LastStageRun" />.</param>
    public void Deconstruct(out bool success, out TBaseNode root, out string lastStageRun)
    {
        success = Success;
        root = Root;
        lastStageRun = LastStageRun;
    }
}