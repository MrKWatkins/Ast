namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    public TNode Parent
    {
        get => parent ?? throw new InvalidOperationException("Node has no parent.");
            
        internal set
        {
            if (parent != null)
            {
                throw new InvalidOperationException("Node is already the child of another node.");
            }

            if (ReferenceEquals(value, this))
            {
                throw new InvalidOperationException("A node cannot be a child of itself.");
            }

            parent = value;
        }
    }
    
    /// <summary>
    /// Does this node have a parent? Nodes will not have parents if they are the root node or they have just been
    /// constructed and not yet added to a parent.
    /// </summary>
    public bool HasParent => parent != null;

    internal void RemoveParent() => parent = null;

    public void RemoveFromParent() => Parent.Children.Remove(This);
        
    public Children<TNode> Children => children ??= new Children<TNode>(This);

    public bool HasChildren => children is { Count: > 0 };
    
    [PublicAPI]
    protected NodeProperties Properties => properties ??= new NodeProperties();
    
    /// <summary>
    /// Moves this node to a new parent.
    /// </summary>
    public void MoveTo(TNode newParent) => newParent.Children.MoveInto(This);

    /// <summary>
    /// Removes this node from it's parent and puts <see cref="other" /> in its place.
    /// </summary>
    public void ReplaceWith(Node<TNode> other) => Parent.Children.Replace(This, (TNode) other);
    
    public IEnumerable<TNode> Ancestors
    {
        get
        {
            var current = parent;
            while (current != null)
            {
                yield return current;
                current = current.parent;
            }
        }
    }
    [Pure]
    [PublicAPI]
    protected IEnumerable<TNode> ThisAnd(IEnumerable<TNode> and)
    {
        yield return This;
            
        foreach (var node in and)
        {
            yield return node;
        }
    }

    public IEnumerable<TNode> ThisAndAncestors => ThisAnd(Ancestors);

    /// <summary>
    /// The root node, i.e. the highest parent above this node. Returns this node if it is the root, i.e. it has no parents.
    /// </summary>
    public TNode Root => ThisAndAncestors.Last();

    [Pure]
    private int GetIndexOfSelf() => Parent.Children.IndexOf(This);  // Can never be -1.

    public TNode? NextSibling
    {
        get
        {
            if (!HasParent)
            {
                return null;
            }
                
            var indexOfSelf = GetIndexOfSelf();
                
            return indexOfSelf < Parent.Children.Count - 1 ? Parent.Children[indexOfSelf + 1] : null;
        }
    }

    public IEnumerable<TNode> NextSiblings => HasParent ? Parent.Children.Skip(GetIndexOfSelf() + 1) : Enumerable.Empty<TNode>();

    public IEnumerable<TNode> ThisAndNextSiblings => ThisAnd(NextSiblings);

    public TNode? PreviousSibling
    {
        get
        {
            if (!HasParent)
            {
                return null;
            }
                
            var indexOfSelf = GetIndexOfSelf();
                
            return indexOfSelf > 0 ? Parent.Children[indexOfSelf - 1] : null;
        }
    }

    /// <summary>
    /// Returns the siblings that come before this node. Returns in closest to this order first, e.g. if parent
    /// has children a, b, c, d then c.PreviousSiblings will return b, a.
    /// </summary>
    public IEnumerable<TNode> PreviousSiblings
    {
        get
        {
            if (!HasParent)
            {
                yield break;
            }
                
            var f = GetIndexOfSelf() - 1;
            while (f >= 0)
            {
                yield return Parent.Children[f];
                f--;
            }
        }
    }

    public IEnumerable<TNode> ThisAndPreviousSiblings => ThisAnd(PreviousSiblings);

    /// <summary>
    /// Enumerates all descendents of this node in depth first order.
    /// </summary>
    public IEnumerable<TNode> Descendents => Children.SelectMany(c => c.ThisAndDescendents);

    /// <summary>
    /// Enumerates this node then all descendents of this node in depth first order.
    /// </summary>
    public IEnumerable<TNode> ThisAndDescendents => ThisAnd(Descendents);
}