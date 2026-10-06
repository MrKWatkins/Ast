namespace MrKWatkins.Ast.Visiting;

/// <summary>
/// Fluent interface to build a <see cref="CompositeVisitor{TContext, TBaseNode, TResult}"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public interface ICompositeVisitorBuilder<TContext, TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Add a visitor for the base node type. Useful to provide a fallback visitor when there is no visitor for the
    /// specific node type registered.
    /// </summary>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="InvalidOperationException">
    /// If a visitor has already been registered for the base node type, or if <paramref name="visitor" /> has already been registered
    /// with a composite visitor.
    /// </exception>
    [MustUseReturnValue]
    ICompositeVisitorBuilder<TContext, TBaseNode, TResult> With(Visitor<TContext, TBaseNode, TResult> visitor);

    /// <summary>
    /// Add a visitor for the specific node type <typeparamref name="TNode"/>. This can be a base node type which will
    /// be used if there is no visitor for the specific node type registered.
    /// </summary>
    /// <typeparam name="TNode">The type of the node <paramref name="visitor"/> visits.</typeparam>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="InvalidOperationException">
    /// If a visitor has already been registered for <typeparamref name="TNode" />, or if <paramref name="visitor" /> has already been registered
    /// with a composite visitor.
    /// </exception>
    [MustUseReturnValue]
    ICompositeVisitorBuilder<TContext, TBaseNode, TResult> With<TNode>(NodeVisitor<TContext, TBaseNode, TNode, TResult> visitor)
        where TNode : TBaseNode;

    /// <summary>
    /// Builds the <see cref="CompositeVisitor{TContext, TBaseNode, TResult}"/>.
    /// </summary>
    /// <returns>The <see cref="CompositeVisitor{TContext, TBaseNode, TResult}"/>.</returns>
    /// <exception cref="InvalidOperationException">If no visitors have been registered.</exception>
    [Pure]
    CompositeVisitor<TContext, TBaseNode, TResult> ToVisitor();
}

/// <summary>
/// Fluent interface to build a <see cref="CompositeVisitor{TBaseNode, TResult}"/>.
/// </summary>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
/// <typeparam name="TResult">The type of the result of visiting a node.</typeparam>
public interface ICompositeVisitorBuilder<TBaseNode, TResult>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Add a visitor for the base node type. Useful to provide a fallback visitor when there is no visitor for the
    /// specific node type registered.
    /// </summary>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="InvalidOperationException">
    /// If a visitor has already been registered for the base node type, or if <paramref name="visitor" /> has already been registered
    /// with a composite visitor.
    /// </exception>
    [MustUseReturnValue]
    ICompositeVisitorBuilder<TBaseNode, TResult> With(Visitor<TBaseNode, TResult> visitor);

    /// <summary>
    /// Add a visitor for the specific node type <typeparamref name="TNode"/>. This can be a base node type which will
    /// be used if there is no visitor for the specific node type registered.
    /// </summary>
    /// <typeparam name="TNode">The type of the node <paramref name="visitor"/> visits.</typeparam>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The fluent builder.</returns>
    /// <exception cref="InvalidOperationException">
    /// If a visitor has already been registered for <typeparamref name="TNode" />, or if <paramref name="visitor" /> has already been registered
    /// with a composite visitor.
    /// </exception>
    [MustUseReturnValue]
    ICompositeVisitorBuilder<TBaseNode, TResult> With<TNode>(NodeVisitor<TBaseNode, TNode, TResult> visitor)
        where TNode : TBaseNode;

    /// <summary>
    /// Builds the <see cref="CompositeVisitor{TBaseNode, TResult}"/>.
    /// </summary>
    /// <returns>The <see cref="CompositeVisitor{TBaseNode, TResult}"/>.</returns>
    /// <exception cref="InvalidOperationException">If no visitors have been registered.</exception>
    [Pure]
    CompositeVisitor<TBaseNode, TResult> ToVisitor();
}