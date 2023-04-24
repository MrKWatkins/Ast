namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private TNode? parent;
    private Children<TNode>? children;
    
    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        this.children = new Children<TNode>(This, children);
    }
    
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
    
    /// <summary>
    /// Moves this node to a new parent.
    /// </summary>
    public void MoveTo(TNode newParent) => newParent.Children.MoveInto(This);

    /// <summary>
    /// Removes this node from it's parent and puts another node in its place.
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

    public void AddNextSibling(TNode nextSibling)
    {
        if (!HasParent)
        {
            throw new InvalidOperationException("Cannot add a next sibling to the root node.");
        }

        var index = GetIndexOfSelf();
        Parent.Children.Insert(index + 1, nextSibling);
    }
    
    public TNode? RemoveNextSibling()
    {
        var nextSibling = NextSibling;
        nextSibling?.RemoveFromParent();
        return nextSibling;
    }

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

    public void AddPreviousSibling(TNode previousSibling)
    {
        if (!HasParent)
        {
            throw new InvalidOperationException("Cannot add a previous sibling to the root node.");
        }

        var index = GetIndexOfSelf();
        Parent.Children.Insert(index, previousSibling);
    }
    
    public TNode? RemovePreviousSibling()
    {
        var previousSibling = PreviousSibling;
        previousSibling?.RemoveFromParent();
        return previousSibling;
    }

    /// <summary>
    /// Enumerates all descendents of this node in depth first order.
    /// </summary>
    public IEnumerable<TNode> Descendents => Traverse.DepthFirstPreOrder(This, false);

    /// <summary>
    /// Enumerates this node then all descendents of this node in depth first order.
    /// </summary>
    public IEnumerable<TNode> ThisAndDescendents => Traverse.DepthFirstPreOrder(This);
}