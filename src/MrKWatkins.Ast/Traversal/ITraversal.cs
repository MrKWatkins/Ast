namespace MrKWatkins.Ast.Traversal;

/// <summary>
/// Strategy for traversing nodes in a tree.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public interface ITraversal<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Enumerates over a node and its descendants.
    /// </summary>
    /// <param name="root">
    /// The root node to enumerate over.
    /// </param>
    /// <param name="includeRoot">
    /// Whether to include <paramref name="root" /> in the results or not. Defaults to <c>true</c>.
    /// </param>
    /// <param name="shouldEnumerateDescendants">
    /// Optional function to specify whether the descendants of a given node should be included or not.
    /// If not provided then all descendants will be included.
    /// </param>
    /// <returns>
    /// A lazy <see cref="IEnumerable{T}" /> of the descendants.
    /// </returns>
    [Pure]
    IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendants = null);
}