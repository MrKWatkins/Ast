using System.Collections;

namespace MrKWatkins.Ast;

public sealed class Children<TNode> : IList<TNode>
    where TNode : Node<TNode>
{
    private readonly List<TNode> nodes;
    private readonly TNode parent;

    internal Children(TNode parent)
    {
        this.parent = parent;
        nodes = new List<TNode>();
    }
        
    internal Children(TNode parent, [InstantHandle] IEnumerable<TNode> nodes)
    {
        this.parent = parent;
        this.nodes = nodes.ToList();
        foreach (var node in this.nodes)
        {
            node.Parent = parent;
        }
    }

    public void Add(TNode node)
    {
        node.Parent = parent;
        nodes.Add(node);
    }

    // ReSharper disable once ParameterHidesMember
    public void Add([InstantHandle] IEnumerable<TNode> nodes)
    {
        foreach (var node in nodes)
        {
            Add(node);
        }
    }

    // ReSharper disable once ParameterHidesMember
    public void Add(params TNode[] nodes) => Add((IEnumerable<TNode>) nodes);

    public void Clear()
    {
        foreach (var node in nodes)
        {
            node.RemoveParent();
        }
        nodes.Clear();
    }

    public bool Contains(TNode node) => nodes.Contains(node);

    void ICollection<TNode>.CopyTo(TNode[] array, int arrayIndex) => nodes.CopyTo(array, arrayIndex);

    public bool Remove(TNode node)
    {
        if (nodes.Remove(node))
        {
            node.RemoveParent();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves <paramref name="node" /> from it's current parent (if it has one) into this collection.
    /// </summary>
    public void MoveInto(TNode node)
    {
        if (node.HasParent)
        {
            if (node.Parent == parent)
            {
                throw new InvalidOperationException($"{nameof(node)} is already in this collection.");
            }
            node.RemoveFromParent();
        }
            
        Add(node);
    }

    /// <summary>
    /// Moves <paramref name="nodes" /> from their current parents (if they have one) into this collection.
    /// </summary>
    // ReSharper disable once ParameterHidesMember
    public void MoveInto([InstantHandle] IEnumerable<TNode> nodes)
    {
        foreach (var node in nodes)
        {
            MoveInto(node);
        }
    }

    /// <summary>
    /// Moves <paramref name="nodes" /> from their current parents (if they have one) into this collection.
    /// </summary>
    // ReSharper disable once ParameterHidesMember
    public void MoveInto(params TNode[] nodes) => MoveInto((IEnumerable<TNode>) nodes);

    public int Count => nodes.Count;

    bool ICollection<TNode>.IsReadOnly => false;
        
    public IEnumerator<TNode> GetEnumerator() => nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => nodes.GetEnumerator();

    public int IndexOf(TNode node) => nodes.IndexOf(node);

    public void Insert(int index, TNode node)
    {
        node.Parent = parent;
        nodes.Insert(index, node);
    }

    void IList<TNode>.RemoveAt(int index) => RemoveAt(index);
        
    public TNode RemoveAt(int index)
    {
        var node = nodes[index];
        node.RemoveParent();
        nodes.RemoveAt(index);
        return node;
    }

    public TNode this[int index]
    {
        get => nodes[index];
        set
        {
            var current = nodes[index];
            value.Parent = parent;
            nodes[index] = value;
            current.RemoveParent();
        }
    }

    public void Replace(TNode child, TNode replacement)
    {
        var index = IndexOf(child);
        if (index == -1)
        {
            throw new ArgumentException("Value could not be found.", nameof(child));
        }

        RemoveAt(index);
            
        if (replacement.HasParent)
        {
            replacement.RemoveFromParent();
        }
            
        Insert(index, replacement);
    }

    [Pure]
    public IEnumerable<TChild> OfType<TChild>()
        where TChild : TNode =>
        nodes.OfType<TChild>();

    [Pure]
    public IEnumerable<TNode> ExceptOfType<TChild>()
        where TChild : TNode =>
        nodes.Where(n => n is not TChild);

    [Pure]
    public TChild? SingleOfTypeOrNull<TChild>()
        where TChild : TNode => 
        (TChild?) SingleOfTypeOrNull(typeof(TChild).Name, OfType<TChild>());
    
    // Hand rolling to give a better exception message than SingleOrDefault().
    [Pure]
    private TNode? SingleOfTypeOrNull(string type, [InstantHandle] IEnumerable<TNode> childrenOfType)
    {
        TNode? single = null;
        foreach (var child in childrenOfType)
        {
            if (single == null)
            {
                single = child;
            }
            else
            {
                throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 0 or 1 children of type {type} but found multiple.");
            }
        }
        return single;
    }
        
    [Pure]
    public TChild SingleOfType<TChild>()
        where TChild : TNode => 
        (TChild) SingleOfType(typeof(TChild).Name, OfType<TChild>());

    // Hand rolling to give a better exception message than Single().
    [Pure]
    private TNode SingleOfType(string type, [InstantHandle] IEnumerable<TNode> childrenOfType)
    {
        TNode? single = null;
        foreach (var child in childrenOfType)
        {
            if (single == null)
            {
                single = child;
            }
            else
            {
                throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 1 child of type {type} but found multiple.");
            }
        }
        return single ?? throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 1 child of type {type} but found none.");
    }
}