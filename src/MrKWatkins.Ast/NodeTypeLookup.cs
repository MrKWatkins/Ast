using System.Collections.Concurrent;

namespace MrKWatkins.Ast;

/// <summary>
/// Maps node types to handlers, e.g. listeners or visitors. When no handler is registered for a node's exact type the handler for its
/// closest base type is used. Lookups are cached so that the base type search only happens once per node type.
/// </summary>
internal sealed class NodeTypeLookup<TBaseNode, THandler>
    where TBaseNode : Node<TBaseNode>
    where THandler : class
{
    private readonly ConcurrentDictionary<Type, THandler?> handlers = new();
    private readonly string handlerName;

    internal NodeTypeLookup(string handlerName)
    {
        this.handlerName = handlerName;
    }

    internal int Count => handlers.Count;

    [MustUseReturnValue]
    internal THandler? Get(TBaseNode node)
    {
        var type = node.GetType();
        if (handlers.TryGetValue(type, out var handler))
        {
            return handler;
        }

        // We might have a handler for a base type.
        var baseType = type;
        while (true)
        {
            baseType = baseType.BaseType!;

            if (handlers.TryGetValue(baseType, out handler))
            {
                // We do. Register it for the type to save this loop in future.
                handlers[type] = handler;
                return handler;
            }

            // If we're already on the root type then we won't have a handler and can abort.
            // Save null against the type to avoid this loop in future.
            if (baseType == typeof(TBaseNode))
            {
                handlers[type] = null;
                return null;
            }
        }
    }

    internal void Add<TNode>(THandler handler)
    {
        if (!handlers.TryAdd(typeof(TNode), handler))
        {
            throw new InvalidOperationException($"A {handlerName} has already been registered for {typeof(TNode).SimpleName()}.");
        }
    }
}