namespace MrKWatkins.Ast.Listening;

public sealed class CompositeListener<TBaseNode> : Listener<TBaseNode>, ICompositeListenerBuilder<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly Dictionary<Type, Listener<TBaseNode>?> listeners = new();

    [Pure]
    public static ICompositeListenerBuilder<TBaseNode> Build() => new CompositeListener<TBaseNode>();

    private CompositeListener()
    {
    }

    protected internal override void BeforeListenToNode(TBaseNode node) => GetListener(node)?.BeforeListenToNode(node);
    
    protected internal override void ListenToNode(TBaseNode node) => GetListener(node)?.ListenToNode(node);
    
    protected internal override void AfterListenToNode(TBaseNode node) => GetListener(node)?.AfterListenToNode(node);

    [MustUseReturnValue]
    private Listener<TBaseNode>? GetListener(TBaseNode node)
    {
        var type = node.GetType();
        if (listeners.TryGetValue(type, out var listener))
        {
            return listener;
        }

        // We might have a listener for a base type.
        var baseType = type;
        while (true)
        {
            baseType = baseType.BaseType!;
            
            if (listeners.TryGetValue(baseType, out listener))
            {
                // We do. Register it for the type to save this loop in future.
                listeners[type] = listener;
                return listener;
            }
            
            // If we're already on the root type then we won't have a listener and can abort.
            // Save null against the type to avoid this loop in future.
            if (baseType == typeof(TBaseNode))
            {
                listeners[type] = null;
                return null;
            }
        }
    }

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With(Listener<TBaseNode> listener) 
    {
        if (!listeners.TryAdd(typeof(TBaseNode), listener))
        {
            throw new InvalidOperationException($"A listener has already been registered for {typeof(TBaseNode).Name}.");
        }

        return this;
    }

    [MustUseReturnValue]
    ICompositeListenerBuilder<TBaseNode> ICompositeListenerBuilder<TBaseNode>.With<TNode>(Listener<TBaseNode, TNode> listener) 
    {
        if (!listeners.TryAdd(typeof(TNode), listener))
        {
            throw new InvalidOperationException($"A listener has already been registered for {typeof(TNode).Name}.");
        }

        return this;
    }

    [Pure]
    CompositeListener<TBaseNode> ICompositeListenerBuilder<TBaseNode>.ToListener() => this;
}