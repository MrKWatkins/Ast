namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPostOrder<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPostOrder<TNode>();

    private DepthFirstPostOrder()
    {
    }

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