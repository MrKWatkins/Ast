namespace MrKWatkins.Ast.Traversal;

/// <summary>
/// Strategy for traversing nodes in a tree depth first, post-order, i.e. depth first with parent nodes being enumerated after their children.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Depth-first_search" />
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class DepthFirstPostOrderTraversal<TNode> : ITraversal<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// The singleton <see cref="DepthFirstPostOrderTraversal{TNode}" /> instance.
    /// </summary>
    /// <returns>The depth first, post order traversal.</returns>
    public static readonly DepthFirstPostOrderTraversal<TNode> Instance = new();

    private DepthFirstPostOrderTraversal()
    {
    }

    /// <inheritdoc />
    public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null)
    {
        if (shouldEnumerateDescendents?.Invoke(root) ?? true)
        {
            foreach (var descendent in root.Children.SelectMany(child => Enumerate(child, true, shouldEnumerateDescendents)))
            {
                yield return descendent;
            }
        }

        if (includeRoot)
        {
            yield return root;
        }
    }
}