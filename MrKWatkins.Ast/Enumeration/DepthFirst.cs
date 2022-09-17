namespace MrKWatkins.Ast.Enumeration;

public abstract class DepthFirst<TNode> : IDescendentEnumerator<TNode>
    where TNode : Node<TNode>
{
    private protected DepthFirst()
    {
    }

    public abstract IEnumerable<TNode> Enumerate(TNode root, bool includeRoot = true);
    
    [Pure]
    protected IEnumerable<TNode> EnumerateChildrenAndDescendents(TNode node)
    {
        // ReSharper disable once ForCanBeConvertedToForeach - using a for loop so Children can be mutated whilst we enumerate.
        for (var f = 0; f < node.Children.Count; f++)
        {
            var countBefore = node.Children.Count;
            var child = node.Children[f];
            foreach (var descendent in Enumerate(child))
            {
                yield return descendent;
            }

            if (countBefore == node.Children.Count)
            {
                continue;
            }
            
            // Children has been mutated. Try and work out what is going on.
            var indexOfChild = node.Children.IndexOf(child);
            if (indexOfChild == -1)
            {
                // child has been removed from the tree. If the count has changed by one assume that's the only change.
                // (e.g. Ignore child and another node being removed, and yet another added) Decrement f so we continue
                // enumeration on it's (previously) next sibling.
                if (node.Children.Count == countBefore - 1)
                {
                    f--;
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Node {child} has been removed from parent during enumeration at the same time as other mutations. Cannot determine sensible place to continue enumeration.");
                }
            }
            else
            {
                // Node is still in the tree. Set f to it's current position and continue on as normal.
                f = indexOfChild;
            }
        }
    }
}