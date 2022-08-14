using System.Reflection;

namespace MrKWatkins.Ast;

public abstract class Node<TType, TNode>
    where TType : struct, Enum
    where TNode : Node<TType, TNode>
{
    static Node()
    {
        if (typeof(TType).GetCustomAttribute<FlagsAttribute>() != null)
        {
            throw new ArgumentException($"{nameof(TType)} cannot be a flags enum.", nameof(TType));
        }
    }
        
    protected Node()
    {
        Children = new Children<TType, TNode>(This);
    }

    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        Children = new Children<TType, TNode>(This, children);
    }
        
    private TNode This => (TNode) this;
    
    // Not using Type for the name as that will most likely get used a lot in compilers.
    public abstract TType NodeType { get; }

    private NodeProperties? properties;
        
    [PublicAPI]
    protected NodeProperties Properties => properties ??= new NodeProperties();

    private TNode? parent;
        
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

    internal void RemoveParent() => parent = null;

    public void RemoveFromParent() => Parent.Children.Remove(This);

    /// <summary>
    /// Moves this node to a new parent.
    /// </summary>
    public void MoveTo(TNode newParent) => newParent.Children.MoveInto(This);

    /// <summary>
    /// Removes this node from it's parent and puts <see cref="other" /> in its place.
    /// </summary>
    public void ReplaceWith(Node<TType, TNode> other) => Parent.Children.Replace(This, (TNode) other);

    /// <summary>
    /// Does this node have a parent? Nodes will not have parents if they are the root node or they have just been
    /// constructed and not yet added to a parent.
    /// </summary>
    public bool HasParent => parent != null;

    public Children<TType, TNode> Children { get; }

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

    [Pure]
    public TNode Copy() => Copy(NodeFactory<TType, TNode>.Default);
        
    [Pure]
    public TNode Copy(INodeFactory<TType, TNode> nodeFactory)
    {
        var copy = nodeFactory.Create(NodeType);
        copy.properties = Properties.Copy();
        copy.Children.Add(Children.Select(c => c.Copy(nodeFactory)));
        return copy;
    }

    public override string ToString() => NodeType.ToString();
}