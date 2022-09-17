namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPostOrder<TNode> : DepthFirst<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPostOrder<TNode>();

    private DepthFirstPostOrder()
    {
    }

    public override IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true)
    {
        foreach (var descendent in EnumerateChildrenAndDescendents(root))
        {
            yield return descendent;
        }
        
        if (includeRoot)
        {
            yield return root;
        }
    }
}