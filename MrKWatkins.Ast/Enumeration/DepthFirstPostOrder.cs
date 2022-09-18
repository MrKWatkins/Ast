namespace MrKWatkins.Ast.Enumeration;

public sealed class DepthFirstPostOrder<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new DepthFirstPostOrder<TNode>();

    private DepthFirstPostOrder()
    {
    }

    public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true)
    {
        foreach (var descendent in root.Children.SelectMany(child => Enumerate(child)))
        {
            yield return descendent;
        }
        
        if (includeRoot)
        {
            yield return root;
        }
    }
}