using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A stage from a <see cref="Pipeline{TBaseNode}"/>.
/// </summary>
/// <remarks>
/// Stages that do not take a context are thin wrappers over the equivalent <see cref="PipelineStage{TContext, TBaseNode}" /> using
/// <see cref="NoContext" />; all the processing logic lives in the latter.
/// </remarks>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class PipelineStage<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private protected PipelineStage(PipelineStage<NoContext, TBaseNode> inner, Func<TBaseNode, bool> shouldContinue)
    {
        Inner = inner;
        ShouldContinue = shouldContinue;
    }

    internal PipelineStage<NoContext, TBaseNode> Inner { get; }

    /// <summary>
    /// The name of the stage.
    /// </summary>
    public string Name => Inner.Name;

    /// <summary>
    /// Function to run after the stage to determine if the pipeline should move on to the next stage or not.
    /// </summary>
    public Func<TBaseNode, bool> ShouldContinue { get; }

    /// <summary>
    /// The default <see cref="ITraversal{TNode}" /> to use when traversing the tree if not specified by an <see cref="OrderedProcessor{TBaseNode}"/>.
    /// </summary>
    public ITraversal<TBaseNode> DefaultTraversal => Inner.DefaultTraversal;

    /// <summary>
    /// Runs the stage.
    /// </summary>
    /// <param name="root">The root node to run processing on.</param>
    /// <returns>The result of running the stage, including the root node after processing, which may have been replaced.</returns>
    public PipelineStageResult<TBaseNode> Run(TBaseNode root) => Inner.Run(default, root);
}

/// <summary>
/// A stage from a <see cref="Pipeline{TContext, TBaseNode}"/>
/// </summary>
/// <typeparam name="TContext">The type of the processing context.</typeparam>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class PipelineStage<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private protected PipelineStage(string name, Func<TContext, TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal)
    {
        Name = name;
        ShouldContinue = shouldContinue;
        DefaultTraversal = defaultTraversal;
    }

    /// <summary>
    /// The name of the stage.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Function to run after the stage to determine if the pipeline should move on to the next stage or not.
    /// </summary>
    public Func<TContext, TBaseNode, bool> ShouldContinue { get; }

    /// <summary>
    /// The default <see cref="ITraversal{TNode}" /> to use when traversing the tree if not specified by an <see cref="OrderedProcessor{TBaseNode}"/>.
    /// </summary>
    public ITraversal<TBaseNode> DefaultTraversal { get; }

    /// <summary>
    /// Runs the stage.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run processing on.</param>
    /// <returns>The result of running the stage, including the root node after processing, which may have been replaced.</returns>
    /// <exception cref="PipelineException">If a processor or the <see cref="ShouldContinue" /> function throws.</exception>
    public PipelineStageResult<TBaseNode> Run(TContext context, TBaseNode root)
    {
        var newRoot = Process(context, root);

        try
        {
            return new PipelineStageResult<TBaseNode>(ShouldContinue(context, newRoot), newRoot);
        }
        catch (Exception exception)
        {
            throw new PipelineException("Exception occurred executing the should continue function.", Name, exception);
        }
    }

    /// <summary>
    /// Processes the specified tree.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node of the tree.</param>
    /// <returns>The root node, which may have been replaced.</returns>
    private protected abstract TBaseNode Process(TContext context, TBaseNode root);
}