namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPreOrder<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPreOrder<TNode>();

    private DepthFirstPreOrder()
    {
    }

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