namespace MrKWatkins.Ast.Visiting;

/// <summary>
/// A <see cref="Visitor{TContext, TNode, TResult}" /> that only visits nodes of a specific type. Visiting a node of any other type calls
/// <see cref="VisitUnhandledNode" />, which throws by default. Typed visitors are usually combined in a <see cref="CompositeVisitor{TContext, TBaseNode, TResult}" />,
/// which dispatches each node to the visitor for its type.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to visit.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public abstract class NodeVisitor<TContext, TBaseNode, TNode, TResult> : Visitor<TContext, TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected internal sealed override TResult VisitNode(TContext context, TBaseNode node) =>
        node is TNode typedNode
            ? VisitNode(context, typedNode)
            : VisitUnhandledNode(context, node);

    /// <summary>
    /// Called to visit a node of type <typeparamref name="TNode" />.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    protected abstract TResult VisitNode(TContext context, TNode node);

    /// <summary>
    /// Called to visit a node that is not of type <typeparamref name="TNode" />. Throws an <see cref="InvalidOperationException" /> by default;
    /// override to return a fallback value instead.
    /// </summary>
    /// <param name="context">The context object.</param>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    /// <exception cref="InvalidOperationException">By default.</exception>
    protected virtual TResult VisitUnhandledNode(TContext context, TBaseNode node) =>
        throw new InvalidOperationException($"{GetType().SimpleName()} cannot visit nodes of type {node.GetType().SimpleName()}.");
}

/// <summary>
/// A <see cref="Visitor{TNode, TResult}" /> that only visits nodes of a specific type and does not take a context object. Visiting a node of any
/// other type calls <see cref="VisitUnhandledNode" />, which throws by default. Typed visitors are usually combined in a
/// <see cref="CompositeVisitor{TBaseNode, TResult}" />, which dispatches each node to the visitor for its type.
/// </summary>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TNode">The type of the nodes to visit.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public abstract class NodeVisitor<TBaseNode, TNode, TResult> : Visitor<TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    /// <inheritdoc />
    protected internal sealed override TResult VisitNode(TBaseNode node) =>
        node is TNode typedNode
            ? VisitNode(typedNode)
            : VisitUnhandledNode(node);

    /// <summary>
    /// Called to visit a node of type <typeparamref name="TNode" />.
    /// </summary>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    protected abstract TResult VisitNode(TNode node);

    /// <summary>
    /// Called to visit a node that is not of type <typeparamref name="TNode" />. Throws an <see cref="InvalidOperationException" /> by default;
    /// override to return a fallback value instead.
    /// </summary>
    /// <param name="node">The node being visited.</param>
    /// <returns>The result of visiting <paramref name="node" />.</returns>
    /// <exception cref="InvalidOperationException">By default.</exception>
    protected virtual TResult VisitUnhandledNode(TBaseNode node) =>
        throw new InvalidOperationException($"{GetType().SimpleName()} cannot visit nodes of type {node.GetType().SimpleName()}.");
}