namespace MrKWatkins.Ast;

/// <summary>
/// An <see cref="IEqualityComparer{T}" /> that compares <see cref="PropertyNode{TNode}">PropertyNodes</see> by their type and
/// <see cref="Properties" />. The nodes' children, source positions and messages are not compared.
/// </summary>
/// <typeparam name="TNode">The type of the nodes.</typeparam>
public sealed class PropertyNodeComparer<TNode> : IEqualityComparer<TNode>
    where TNode : PropertyNode<TNode>
{
    /// <summary>
    /// The single instance of the <see cref="PropertyNodeComparer{TNode}" />.
    /// </summary>
    /// <returns>The instance.</returns>
    public static PropertyNodeComparer<TNode> Instance { get; } = new();

    private PropertyNodeComparer()
    {
    }

    /// <summary>
    /// Returns <c>true</c> if two nodes have the same type and equal <see cref="Properties" />, <c>false</c> otherwise. Properties are
    /// equal if the nodes have the same property keys, and the values for each key are single or multiple valued to match, have the
    /// same type and are equal, using <see cref="object.Equals(object)" /> for single values and element-wise for multiple values.
    /// </summary>
    /// <param name="x">The first node.</param>
    /// <param name="y">The second node.</param>
    /// <returns><c>true</c> if the nodes have the same type and equal properties, <c>false</c> otherwise.</returns>
    [Pure]
    public bool Equals(TNode? x, TNode? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.GetType() == y.GetType() && Properties.Equal(x.PropertiesOrNull, y.PropertiesOrNull);
    }

    /// <summary>
    /// Returns a hash code for the specified node consistent with <see cref="Equals(TNode, TNode)" />, based on the node's type and
    /// number of properties.
    /// </summary>
    /// <param name="obj">The node.</param>
    /// <returns>A hash code for the node.</returns>
    [Pure]
    public int GetHashCode(TNode obj) => HashCode.Combine(obj.GetType(), obj.PropertiesOrNull?.Count ?? 0);
}