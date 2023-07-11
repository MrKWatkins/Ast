namespace MrKWatkins.Ast;

/// <summary>
/// Factory to create nodes for a tree.
/// </summary>
/// <typeparam name="TNode">The base type of nodes to create.</typeparam>
public interface INodeFactory<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Creates a node of the specified type. The node must inherit from <typeparamref name="TNode" />.
    /// </summary>
    /// <param name="nodeType">The type of node to create.</param>
    /// <returns>The new node.</returns>
    /// <exception cref="ArgumentException"><paramref name="nodeType"/> is not a <typeparamref name="TNode"/>.</exception>
    [Pure]
    TNode Create(Type nodeType);

    /// <summary>
    /// Creates a node of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of node to create.</typeparam>
    /// <returns>The new node.</returns>
    [Pure]
    T Create<T>()
        where T : TNode
        => (T)Create(typeof(T));
}