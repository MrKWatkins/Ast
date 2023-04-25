using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// Static helper methods for traversing trees.
    /// </summary>
    public static class Traverse
    {
        /// <summary>
        /// Enumerates over a node and its descendents breadth first.
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
        /// A lazy <see cref="IEnumerable{T}" /> of the descendents in breadth first order.
        /// </returns>
        /// <seealso href="https://en.wikipedia.org/wiki/Breadth-first_search" />
        [Pure]
        public static IEnumerable<TNode> BreadthFirst(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null) => 
            BreadthFirstTraversal<TNode>.Instance.Enumerate(root, includeRoot, shouldEnumerateDescendents);
        
        /// <summary>
        /// Enumerates over a node and its descendents depth first, pre-order.
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
        /// A lazy <see cref="IEnumerable{T}" /> of the descendents in depth first pre-order.
        /// </returns>
        /// <seealso cref="DepthFirstPreOrderTraversal{TNode}"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Depth-first_search" />
        [Pure]
        public static IEnumerable<TNode> DepthFirstPreOrder(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null) => 
            DepthFirstPreOrderTraversal<TNode>.Instance.Enumerate(root, includeRoot, shouldEnumerateDescendents);

        /// <summary>
        /// Enumerates over a node and its descendents depth first, post-order.
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
        /// A lazy <see cref="IEnumerable{T}" /> of the descendents in depth first post-order.
        /// </returns>
        /// <seealso cref="DepthFirstPostOrderTraversal{TNode}"/>
        /// <seealso href="https://en.wikipedia.org/wiki/Depth-first_search" />
        [Pure]
        public static IEnumerable<TNode> DepthFirstPostOrder(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null) => 
            DepthFirstPostOrderTraversal<TNode>.Instance.Enumerate(root, includeRoot, shouldEnumerateDescendents);
    }
}