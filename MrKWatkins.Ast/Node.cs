using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

public abstract class Node<TNode>
    where TNode : Node<TNode>
{
    private TNode? parent;
    private Children<TNode>? children;
    private NodeProperties? properties;
    private List<Message>? messages;

    protected Node()
    {
    }

    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        this.children = new Children<TNode>(This, children);
    }
        
    private TNode This => (TNode) this;
    
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

    public IReadOnlyList<Message> Messages => messages != null ? messages : Array.Empty<Message>();

    public bool HasMessages => Messages.Any();

    public bool ThisAndDescendentsHaveMessages => ThisAndDescendentsWithMessages.Any();

    public IEnumerable<TNode> ThisAndDescendentsWithMessages => ThisAndDescendents.Where(n => n.HasMessages);

    public void AddMessage(Message message) => (messages ??= new List<Message>()).Add(message);

    public void AddMessage(MessageLevel level, string text) => AddMessage(new Message(level, text));
    
    public void AddMessage(MessageLevel level, string code, string text) => AddMessage(new Message(level, code, text));

    public IEnumerable<Message> Errors => Messages.Where(m => m.Level == MessageLevel.Error);
    
    public bool HasErrors => Errors.Any();

    public bool ThisAndDescendentsHaveErrors => ThisAndDescendentsWithErrors.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithErrors => ThisAndDescendents.Where(n => n.HasErrors);
    
    public void AddError(string text) => AddMessage(Message.Error(text));
    
    public void AddError(string code, string text) => AddMessage(Message.Error(code, text));
    
    public IEnumerable<Message> Warnings => Messages.Where(m => m.Level == MessageLevel.Warning);

    public bool HasWarnings => Warnings.Any();

    public bool ThisAndDescendentsHaveWarnings => ThisAndDescendentsWithWarnings.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithWarnings => ThisAndDescendents.Where(n => n.HasWarnings);
    
    public void AddWarning(string text) => AddMessage(Message.Warning(text));
    
    public void AddWarning(string code, string text) => AddMessage(Message.Warning(code, text));

    public void AddInfo(string text) => AddMessage(Message.Info(text));
    
    public void AddInfo(string code, string text) => AddMessage(Message.Info(code, text));

    public IEnumerable<Message> Infos => Messages.Where(m => m.Level == MessageLevel.Info);

    public bool HasInfos => Infos.Any();

    public bool ThisAndDescendentsHaveInfos => ThisAndDescendentsWithInfos.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithInfos => ThisAndDescendents.Where(n => n.HasInfos);
    
    public SourcePosition SourcePosition
    {
        get => Properties.GetOrDefault(nameof(SourcePosition), SourcePosition.None);
        set => Properties.Set(nameof(SourcePosition), value);
    }
    
    [Pure]
    public TNode Copy() => Copy(NodeFactory<TNode>.Default);
        
    [Pure]
    public TNode Copy(INodeFactory<TNode> nodeFactory)
    {
        var copy = nodeFactory.Create(GetType());
        copy.properties = properties?.Copy();
        if (children != null)
        {
            copy.Children.Add(children.Select(c => c.Copy(nodeFactory)));
        }
        return copy;
    }

    public override string ToString() => GetType().Name;
}