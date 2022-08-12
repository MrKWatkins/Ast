using System.Linq.Expressions;
using System.Reflection;

namespace MrKWatkins.Ast;

public sealed class NodeFactory<TType, TNode> : INodeFactory<TType, TNode>
    where TType : Enum
    where TNode : Node<TType, TNode>
{
    public static readonly INodeFactory<TType, TNode> Default = new NodeFactory<TType, TNode>();

    // Using a lazy so we get exceptions on Create rather than TypeLoadExceptions via the static constructor blowing up.
    private readonly Lazy<IReadOnlyDictionary<TType, Func<TNode>>> constructorsByNodeType = new(BuildConstructorsByNodeType);
        
    private NodeFactory()
    {
    }

    public TNode Create(TType nodeType)
    {
        var name = $"{typeof(TType).Name}.{nodeType}";
        if (constructorsByNodeType.Value.TryGetValue(nodeType, out var constructor))
        {
            return CallConstructor(name, constructor);
        }
            
        throw new InvalidOperationException($"A {typeof(TNode).Name} for {name} could not be found in the same assembly as {typeof(TNode).Name}.");
    }

    private static IReadOnlyDictionary<TType, Func<TNode>> BuildConstructorsByNodeType()
    {
        var constructorsByNodeType = new Dictionary<TType, Func<TNode>>();
            
        var types = typeof(TNode).Assembly.GetTypes().Where(type => typeof(TNode).IsAssignableFrom(type) && !type.IsAbstract);
        foreach (var type in types)
        {
            var constructorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(c => c.GetParameters().Length == 0);

            if (constructorInfo == null)
            {
                throw new InvalidOperationException($"Could not find a parameterless constructor for the node type {type.Name}.");
            }

            var expression = Expression.Lambda<Func<TNode>>(Expression.New(constructorInfo));
            var constructor = expression.Compile();

            var node = CallConstructor(type.Name, constructor);
                
            TType nodeType;
            try
            {
                nodeType = node.NodeType;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"The node of type {type.Name} threw when calling the {nameof(node.NodeType)} property.", exception);
            }

            if (constructorsByNodeType.ContainsKey(nodeType))
            {
                throw new InvalidOperationException($"Multiple types return a {typeof(TType).Name} value of {nodeType}.");
            }

            constructorsByNodeType[nodeType] = constructor;
        }

        if (constructorsByNodeType.Count == 0)
        {
            throw new InvalidOperationException($"No implementations of {typeof(TNode).Name} found.");
        }
            
        return constructorsByNodeType;
    }

    [Pure]
    private static TNode CallConstructor(string name, Func<TNode> constructor)
    {
        try
        {
            return constructor();
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Exception calling the constructor for {name}.", exception);
        }
    }
}