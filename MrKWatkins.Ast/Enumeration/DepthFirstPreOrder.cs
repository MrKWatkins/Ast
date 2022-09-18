namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPreOrder<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPreOrder<TNode>();

    private DepthFirstPreOrder()
    {
    }

    public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true)
    {
        if (includeRoot)
        {
            yield return root;
        }

        foreach (var descendent in root.Children.SelectMany(child => Enumerate(child)))
        {
            yield return descendent;
        }
    }
}