namespace MrKWatkins.Ast.Enumeration;

public sealed class BreadthFirst<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    public static readonly IDescendentEnumerator<TNode> Instance = new BreadthFirst<TNode>();

    private BreadthFirst()
    {
    }

    public IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true, Func<TNode, bool>? shouldEnumerateDescendents = null)
    {
        // Start by queuing and yielding the root.
        var queue = new Queue<TNode>();
        queue.Enqueue(root);

        if (includeRoot)
        {
            yield return root;
        }

        // While we have items on the list dequeue them then yield and queue their children.
        // This means for each level we will then yield all their children, i.e. giving the level below,
        // whilst at the same time queueing them up so that next time around will will get the level below
        // and so on.
        shouldEnumerateDescendents ??= _ => true;
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (!shouldEnumerateDescendents(node))
            {
                continue;
            }
            
            foreach (var child in node.Children)
            {
                yield return child;
                queue.Enqueue(child);
            }
        }
    }
}