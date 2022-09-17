namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPreOrder<TNode> : DepthFirst<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPreOrder<TNode>();

    private DepthFirstPreOrder()
    {
    }

    public override IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true)
    {
        if (includeRoot)
        {
            yield return root;
        }

        foreach (var descendent in EnumerateChildrenAndDescendents(root))
        {
            yield return descendent;
        }
    }
}