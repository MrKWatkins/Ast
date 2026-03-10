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
    /// Runs the stage, returning the potentially new root via an out parameter.
    /// </summary>
    /// <param name="root">The root node to run processing on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TBaseNode}"/>.</param>
    /// <returns><c>true</c> if the pipeline should proceed to the next stage, <c>false</c> otherwise.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public bool Run(TBaseNode root, out TBaseNode newRoot)
    {
        var (success, resultRoot) = Run(root);
        newRoot = resultRoot;
        return success;
    }

    /// <summary>
    /// Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node.
    /// </summary>
    /// <param name="root">The root node to run processing on.</param>
    /// <returns>A tuple of whether processing should continue and the root node, which may have been replaced by a <see cref="Replacer{TBaseNode}"/>.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public (bool Success, TBaseNode Root) Run(TBaseNode root)
    {
        var newRoot = Process(root);

        try
        {
            return (ShouldContinue(newRoot), newRoot);
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
    /// <returns>The root node, which may have been replaced.</returns>
    private protected abstract TBaseNode Process(TBaseNode root);
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
    /// Runs the stage, returning the potentially new root via an out parameter.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run processing on.</param>
    /// <param name="newRoot">The root node after processing, which may have been replaced by a <see cref="Replacer{TContext, TBaseNode}"/>.</param>
    /// <returns><c>true</c> if the pipeline should proceed to the next stage, <c>false</c> otherwise.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot)
    {
        var (success, resultRoot) = Run(context, root);
        newRoot = resultRoot;
        return success;
    }

    /// <summary>
    /// Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node.
    /// </summary>
    /// <param name="context">The processing context.</param>
    /// <param name="root">The root node to run processing on.</param>
    /// <returns>A tuple of whether processing should continue and the root node, which may have been replaced by a <see cref="Replacer{TContext, TBaseNode}"/>.</returns>
    /// <exception cref="PipelineException">If an unhandled exception occurs during processing.</exception>
    public (bool Success, TBaseNode Root) Run(TContext context, TBaseNode root)
    {
        var newRoot = Process(context, root);

        try
        {
            return (ShouldContinue(context, newRoot), newRoot);
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
