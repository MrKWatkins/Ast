using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// A stage from a <see cref="Pipeline{TBaseNode}"/>
/// </summary>
/// <typeparam name="TBaseNode">The type of nodes in the tree.</typeparam>
public abstract class PipelineStage<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private protected PipelineStage(string name, Func<TBaseNode, bool> shouldContinue, ITraversal<TBaseNode> defaultTraversal)
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
    public Func<TBaseNode, bool> ShouldContinue { get; }

    /// <summary>
    /// The default <see cref="ITraversal{TNode}" /> to use when traversing the tree if not specified by an <see cref="OrderedProcessor{TBaseNode}"/>.
    /// </summary>
    public ITraversal<TBaseNode> DefaultTraversal { get; }

    /// <summary>
    /// Runs the stage.
    /// </summary>
    /// <param name="root">The root node to run processing on.</param>
    /// <returns><c>true</c> if the pipeline should proceed to the next stage, <c>false</c> otherwise.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public bool Run(TBaseNode root)
    {
        Process(root);

        try
        {
            return ShouldContinue(root);
        }
        catch (Exception exception)
        {
            throw new PipelineException("Exception occurred executing the should continue function.", Name, exception);
        }
    }

    /// <summary>
    /// Processes the specified tree.
    /// </summary>
    /// <param name="root">The root node of the tree.</param>
    private protected abstract void Process(TBaseNode root);
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
    /// <returns><c>true</c> if the pipeline should proceed to the next stage, <c>false</c> otherwise.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public bool Run(TContext context, TBaseNode root)
    {
        Process(context, root);

        try
        {
            return ShouldContinue(context, root);
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
    private protected abstract void Process(TContext context, TBaseNode root);
}