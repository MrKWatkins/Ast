namespace MrKWatkins.Ast.Enumeration;

public interface IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Enumerates over a node and its descendents.
    /// </summary>
    /// <param name="root">
    /// The root node to enumerate over.
    /// </param>
    /// <param name="includeRoot">
    /// Whether to include <paramref name="root" /> in the results or not. Defaults to <c>true</c>.
    /// </param>
    /// <param name="shouldEnumerateDescendents">
    /// Optional function to specify whether the descendents of a given node should be included or not.
    /// If not provided then all descendents will be included.
    /// </param>
    /// <returns>
    /// A lazy <see cref="IEnumerable{T}" /> of the descendents.
    /// </returns>
    [Pure]
    IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null);
}