namespace MrKWatkins.Ast.Visiting;

/// <summary>
/// A visitor for a syntax tree. A visitor is called for a node and returns a value for it. Unlike a <see cref="Listening.Listener{TContext, TNode}" />,
/// which walks the whole tree itself, a visitor controls the walk: it visits whichever children it needs, in whatever order it needs, and combines
/// their results into its own. That makes visitors the natural fit for evaluating or transforming a tree into a value, especially when not every
/// child should be visited, e.g. short-circuit evaluation.
/// </summary>
/// <remarks>
/// Exceptions are not handled; if the visitor throws then the exception will escape from the <see cref="Visit"/> method and no
/// further nodes will be visited.
/// </remarks>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TNode">The type of the nodes to visit.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public abstract class Visitor<TContext, TNode, TResult>
    where TNode : Node<TNode>
{
    private Visitor<TContext, TNode, TResult>? owner;

    /// <summary>
    /// Visits the specified node and returns the result.
    /// </summary>
    /// <remarks>
    /// If this visitor has been registered with a <see cref="CompositeVisitor{TContext, TBaseNode, TResult}" /> then the node will be dispatched
    /// through that composite, so that a visitor for one node type can visit children of other node types. Visitors should therefore always use
    /// this method, rather than <see cref="VisitNode" />, to visit children.
    /// </remarks>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node to visit.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    public TResult Visit(TContext context, TNode node) => Root.VisitNode(context, node);

    /// <summary>
    /// Visits all the children of the specified node in order and returns their results.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node whose children should be visited.</param>
    /// <returns>The results of visiting each child of <paramref name="node" />, in the same order as the children.</returns>
    protected TResult[] VisitChildren(TContext context, TNode node)
    {
        var children = node.Children;
        var results = new TResult[children.Count];
        for (var f = 0; f < results.Length; f++)
        {
            results[f] = Visit(context, children[f]);
        }

        return results;
    }

    /// <summary>
    /// Called to visit a node.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    protected internal abstract TResult VisitNode(TContext context, TNode node);

    /// <summary>
    /// The visitor that nodes should be dispatched through: the outermost <see cref="CompositeVisitor{TContext, TBaseNode, TResult}" /> this visitor
    /// has been registered with, or this visitor if it has not been registered with one.
    /// </summary>
    private Visitor<TContext, TNode, TResult> Root => owner?.Root ?? this;

    internal bool HasOwner => owner != null;

    internal void SetOwner(Visitor<TContext, TNode, TResult> compositeVisitor) => owner = compositeVisitor;
}

/// <summary>
/// A <see cref="Visitor{TContext, TNode, TResult}" /> that does not take a context object. Any state the visitor needs must be held by the
/// visitor itself.
/// </summary>
/// <remarks>
/// Exceptions are not handled; if the visitor throws then the exception will escape from the <see cref="Visit(TNode)"/> method and no
/// further nodes will be visited.
/// </remarks>
/// <typeparam name="TNode">The type of the nodes to visit.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public abstract class Visitor<TNode, TResult> : Visitor<NoContext, TNode, TResult>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Visits the specified node and returns the result.
    /// </summary>
    /// <remarks>
    /// If this visitor has been registered with a <see cref="CompositeVisitor{TBaseNode, TResult}" /> then the node will be dispatched
    /// through that composite, so that a visitor for one node type can visit children of other node types. Visitors should therefore always use
    /// this method, rather than <see cref="VisitNode(TNode)" />, to visit children.
    /// </remarks>
    /// <param name="node">The node to visit.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    public TResult Visit(TNode node) => Visit(default, node);

    /// <summary>
    /// Visits all the children of the specified node in order and returns their results.
    /// </summary>
    /// <param name="node">The node whose children should be visited.</param>
    /// <returns>The results of visiting each child of <paramref name="node" />, in the same order as the children.</returns>
    protected TResult[] VisitChildren(TNode node) => VisitChildren(default, node);

    /// <inheritdoc />
    protected internal sealed override TResult VisitNode(NoContext context, TNode node) => VisitNode(node);

    /// <summary>
    /// Called to visit a node.
    /// </summary>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    protected internal abstract TResult VisitNode(TNode node);
}