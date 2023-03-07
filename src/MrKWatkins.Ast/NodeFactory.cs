using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace MrKWatkins.Ast;

public sealed class NodeFactory<TNode> : INodeFactory<TNode>
    where TNode : Node<TNode>
{
    public static readonly INodeFactory<TNode> Default = new NodeFactory<TNode>();

    private readonly ConcurrentDictionary<Type, Func<TNode>> constructorsByNodeType = new();
        
    private NodeFactory()
    {
    }

    public TNode Create(Type nodeType) => CallConstructor(nodeType, constructorsByNodeType.GetOrAdd(nodeType, BuildConstructor));

    [Pure]
    private static Func<TNode> BuildConstructor(Type type)
    {
        var constructorInfo = type
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(c => c.GetParameters().Length == 0);

        if (constructorInfo == null)
        {
            throw new InvalidOperationException($"Could not find a parameterless constructor for the node type {type.SimpleName()}.");
        }

        var expression = Expression.Lambda<Func<TNode>>(Expression.New(constructorInfo));
        var constructor = expression.Compile();

        return constructor;
    }

    [Pure]
    private static TNode CallConstructor(Type type, Func<TNode> constructor)
    {
        try
        {
            return constructor();
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Exception calling the constructor for {type.Name}.", exception);
        }
    }
}