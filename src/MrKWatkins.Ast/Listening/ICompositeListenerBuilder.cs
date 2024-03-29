namespace MrKWatkins.Ast.Listening;

/// <summary>
/// Fluent interface to build a <see cref="CompositeListener{TContext, TBaseNode}"/>.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
public interface
    ICompositeListenerBuilder<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    /// <summary>
    /// Add a listener for the base node type. Useful to provide a fallback listener when there is no listener for the
    /// specific node type registered.
    /// </summary>
    /// <param name="listener">The listener.</param>
    /// <returns>The fluent builder.</returns>
    [MustUseReturnValue]
    ICompositeListenerBuilder<TContext, TBaseNode> With(Listener<TContext, TBaseNode> listener);

    /// <summary>
    /// Add a listener for the specific node type <typeparamref name="TNode"/>. This can be a base node type which will
    /// be used if there is no listener for the specific node type registered.
    /// </summary>
    /// <typeparam name="TNode">The type of the node <paramref name="listener"/> listens to.</typeparam>
    /// <param name="listener">The listener.</param>
    /// <returns>The fluent builder.</returns>
    [MustUseReturnValue]
    ICompositeListenerBuilder<TContext, TBaseNode> With<TNode>(Listener<TContext, TBaseNode, TNode> listener)
        where TNode : TBaseNode;

    /// <summary>
    /// Builds the <see cref="CompositeListener{TContext, TBaseNode}"/>.
    /// </summary>
    /// <returns>The <see cref="CompositeListener{TContext, TBaseNode}"/>.</returns>
    /// <exception cref="InvalidOperationException">If no listeners have been registered or multiple listeners for the same type have been registered.</exception>
    [Pure]
    CompositeListener<TContext, TBaseNode> ToListener();
}