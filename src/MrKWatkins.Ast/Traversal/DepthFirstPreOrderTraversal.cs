namespace MrKWatkins.Ast.Traversal;

/// <summary>
/// Strategy for traversing nodes in a tree depth first, pre-order, i.e. depth first with parent nodes being enumerated before their children.
/// </summary>
/// <remarks>
/// This is the default traversal for the library.
/// </remarks>
/// <seealso href="https://en.wikipedia.org/wiki/Depth-first_search" />
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class DepthFirstPreOrderTraversal<TNode> : ITraversal<TNode>
    where TNode : Node<TNode>
{
    /// <summary>
    /// The singleton <see cref="DepthFirstPreOrderTraversal{TNode}" /> instance.
    /// </summary>
    /// <returns>The depth first, pre-order traversal.</returns>
    public static readonly DepthFirstPreOrderTraversal<TNode> Instance = new();

    private DepthFirstPreOrderTraversal()
    {
    }

    /// <inheritdoc />
    public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null)
    {
        if (includeRoot)
        {
            yield return root;
        }

        if (shouldEnumerateDescendents?.Invoke(root) ?? true)
        {
            foreach (var descendent in root.Children.SelectMany(child => Enumerate(child, true, shouldEnumerateDescendents)))
            {
                yield return descendent;
            }
        }
    }
}