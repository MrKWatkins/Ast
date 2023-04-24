namespace MrKWatkins.Ast.Listening;

/// <summary>
/// A <see cref="Listener{TNode}" /> built from multiple other listeners that listen to specific node types. When a node is reached
/// the listener with the most specific type for the node will be used. Only a single listener will ever listen to a node. If no suitable
/// listener is found the node will be ignored but it's descendents will still be listened to.
/// </summary>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
public sealed class CompositeListener<TBaseNode> : Listener<TBaseNode>, ICompositeListenerBuilder<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly ListenerLookup<TBaseNode, Listener<TBaseNode>> listeners = new();
    
    /// <summary>
    /// Fluent interface to build a <see cref="CompositeListener{TBaseNode}"/>.
    /// </summary>
    /// <returns>A fluent builder.</returns>
    [Pure]
    public static ICompositeListenerBuilder<TBaseNode> Build() => new CompositeListener<TBaseNode>();

    /// <summary>
    /// Fluent interface to build a <see cref="CompositeListenerWithContext{TContext, TBaseNode}"/>.
    /// </summary>
    /// <returns>A fluent builder.</returns>
    [Pure]
    public static ICompositeListenerWithContextBuilder<TContext, TBaseNode> BuildWithContext<TContext>() => new CompositeListenerWithContext<TContext, TBaseNode>();
    
    private CompositeListener()
    {
    }

    /// <inheritdoc />
    protected internal override void BeforeListenToNode(TBaseNode node) => listeners.Get(node)?.BeforeListenToNode(node);

    /// <inheritdoc />
    protected internal override void ListenToNode(TBaseNode node) => listeners.Get(node)?.ListenToNode(node);

    /// <inheritdoc />
    protected internal override void AfterListenToNode(TBaseNode node) => listeners.Get(node)?.AfterListenToNode(node);

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With(Listener<TBaseNode> listener)
    {
        listeners.Add<TBaseNode>(listener);

        return this;
    }

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With<TNode>(Listener<TBaseNode, TNode> listener) 
    {
        listeners.Add<TNode>(listener);

        return this;
    }

    [Pure]
    CompositeListener<TBaseNode> ICompositeListenerBuilder<TBaseNode>.ToListener()
    {
        if (listeners.Count == 0)
        {
            throw new InvalidOperationException("No listeners have been registered.");
        }
        return this;
    }
}