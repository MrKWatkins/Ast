using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace MrKWatkins.Ast;

/// <summary>
/// Default implementation of <see cref="INodeFactory{TNode}" />. Nodes to be created must have a parameterless constructor
/// which can be public or non-public.
/// </summary>
/// <typeparam name="TNode">The base type of nodes to create.</typeparam>
public sealed class DefaultNodeFactory<TNode> : INodeFactory<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Singleton instance of <see cref="DefaultNodeFactory{TNode}" />.
    /// </summary>
    public static readonly INodeFactory<TNode> Instance = new DefaultNodeFactory<TNode>();

    private readonly ConcurrentDictionary<Type, Func<TNode>> constructorsByNodeType = new();
        
    private DefaultNodeFactory()
    {
    }

    /// <summary>
    /// Creates a node of the specified type. The node must inherit from <typeparamref name="TNode" />.
    /// </summary>
    /// <param name="nodeType">The type of node to create.</param>
    /// <returns>The new node.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="nodeType"/> is not a <typeparamref name="TNode"/> or it does not have a parameterless constructor.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// An exception occurred creating <paramref name="nodeType"/>.
    /// </exception>
    public TNode Create(Type nodeType) => CallConstructor(nodeType, constructorsByNodeType.GetOrAdd(nodeType, BuildConstructor));

    [Pure]
    private static Func<TNode> BuildConstructor(Type nodeType)
    {
        if (!nodeType.IsAssignableTo(typeof(TNode)))
        {
            throw new ArgumentException($"{nodeType.SimpleName()} is not a {typeof(TNode).SimpleName()}.", nameof(nodeType));
        }
        
        var constructorInfo = nodeType
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(c => c.GetParameters().Length == 0);

        if (constructorInfo == null)
        {
            throw new ArgumentException($"Could not find a parameterless constructor for the node type {nodeType.SimpleName()}.", nameof(nodeType));
        }

        var expression = Expression.Lambda<Func<TNode>>(Expression.New(constructorInfo));
        var constructor = expression.Compile();

        return constructor;
    }

    [Pure]
    private static TNode CallConstructor(Type nodeType, Func<TNode> constructor)
    {
        try
        {
            return constructor();
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Exception calling the constructor for {nodeType.SimpleName()}.", exception);
        }
    }
}