namespace MrKWatkins.Ast.Listening;

internal sealed class ListenerLookup<TBaseNode, TListener>
    where TBaseNode : Node<TBaseNode>
    where TListener : class
{
    private readonly Dictionary<Type, TListener?> listeners = new();

    internal int Count => listeners.Count;
    
    [MustUseReturnValue]
    internal TListener? Get(TBaseNode node)
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

    internal void Add<TNode>(TListener listener)
    {
        if (!listeners.TryAdd(typeof(TNode), listener))
        {
            throw new InvalidOperationException($"A listener has already been registered for {typeof(TNode).Name}.");
        }
    }
}