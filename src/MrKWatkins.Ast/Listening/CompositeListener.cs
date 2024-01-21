namespace MrKWatkins.Ast.Listening;

/// <summary>
/// A <see cref="Listener{TContext, TNode}" /> built from multiple other listeners that listen to specific node types. When a node is reached the listener with the
/// most specific type for the node will be used. Only a single listener will ever listen to a node. If no suitable listener is found the node will be ignored but
/// it's descendents will still be listened to.
/// </summary>
/// <typeparam name="TContext">The type of the context object.</typeparam>
/// <typeparam name="TBaseNode">The base type of all nodes in the tree.</typeparam>
public sealed class CompositeListener<TContext, TBaseNode> : Listener<TContext, TBaseNode>, ICompositeListenerBuilder<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly ListenerLookup<TBaseNode, Listener<TContext, TBaseNode>> listeners = new();

    /// <summary>
    /// Fluent interface to build a <see cref="CompositeListener{TContext, TBaseNode}"/>.
    /// </summary>
    /// <returns>A fluent builder.</returns>
    [Pure]
    public static ICompositeListenerBuilder<TContext, TBaseNode> Build() => new CompositeListener<TContext, TBaseNode>();

    internal CompositeListener()
    {
    }

    /// <inheritdoc />
    protected internal override void BeforeListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.BeforeListenToNode(context, node);

    /// <inheritdoc />
    protected internal override void ListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.ListenToNode(context, node);

    /// <inheritdoc />
    protected internal override void AfterListenToNode(TContext context, TBaseNode node) => listeners.Get(node)?.AfterListenToNode(context, node);

    [MustUseReturnValue]
    ICompositeListenerBuilder<TContext, TBaseNode> ICompositeListenerBuilder<TContext, TBaseNode>.With(Listener<TContext, TBaseNode> listener)
    {
        listeners.Add<TBaseNode>(listener);

        return this;
    }

    [MustUseReturnValue]
    ICompositeListenerBuilder<TContext, TBaseNode> ICompositeListenerBuilder<TContext, TBaseNode>.With<TNode>(Listener<TContext, TBaseNode, TNode> listener)
    {
        listeners.Add<TNode>(listener);

        return this;
    }

    [Pure]
    CompositeListener<TContext, TBaseNode> ICompositeListenerBuilder<TContext, TBaseNode>.ToListener()
    {
        if (listeners.Count == 0)
        {
            throw new InvalidOperationException("No listeners have been registered.");
        }

        return this;
    }
}