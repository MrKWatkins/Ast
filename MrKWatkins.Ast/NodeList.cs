using System.Collections;

// ReSharper disable AnnotationConflictInHierarchy
namespace MrKWatkins.Ast;

// TODO: More tests!
public sealed class NodeList<TType, TNode> : IList<TNode>
    where TType : Enum
    where TNode : Node<TType, TNode>
{
    private readonly List<TNode> nodes;
    private readonly TNode parent;

    internal NodeList(TNode parent)
        : this(parent, Enumerable.Empty<TNode>())
    {
    }
        
    internal NodeList(TNode parent, [InstantHandle] IEnumerable<TNode> nodes)
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
        (node ?? throw new ArgumentNullException(nameof(node))).Parent = parent;
        nodes.Add(node);
    }

    // ReSharper disable once ParameterHidesMember
    public void Add([InstantHandle] IEnumerable<TNode> nodes)
    {
        foreach (var node in nodes ?? throw new ArgumentNullException(nameof(nodes)))
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

    public bool Contains(TNode node) => nodes.Contains(node ?? throw new ArgumentNullException(nameof(node)));

    void ICollection<TNode>.CopyTo(TNode[] array, int arrayIndex) => nodes.CopyTo(array, arrayIndex);

    public bool Remove(TNode node)
    {
        if (nodes.Remove(node ?? throw new ArgumentNullException(nameof(node))))
        {
            node.RemoveParent();
            return true;
        }

        return false;
    }

    public void Move(TNode node)
    {
        if (node == null)
        {
            throw new ArgumentNullException(nameof(node)); 
        }

        if (node.HasParent)
        {
            node.RemoveFromParent();
        }
            
        Add(node);
    }

    // ReSharper disable once ParameterHidesMember
    public void Move([InstantHandle] IEnumerable<TNode> nodes)
    {
        foreach (var node in nodes ?? throw new ArgumentNullException(nameof(nodes)))
        {
            Move(node);
        }
    }

    // ReSharper disable once ParameterHidesMember
    public void Move(params TNode[] nodes) => Move((IEnumerable<TNode>) nodes);

    public int Count => nodes.Count;

    bool ICollection<TNode>.IsReadOnly => false;
        
    public IEnumerator<TNode> GetEnumerator() => nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => nodes.GetEnumerator();

    public int IndexOf(TNode node) => nodes.IndexOf(node ?? throw new ArgumentNullException(nameof(node)));

    public void Insert(int index, TNode node)
    {
        (node ?? throw new ArgumentNullException(nameof(node))).Parent = parent;
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

    public void Replace(TNode node, TNode replacement)
    {
        var index = IndexOf(node);
        if (index == -1)
        {
            throw new InvalidOperationException($"{nameof(node)} could not be found.");
        }

        RemoveAt(index);
            
        if (replacement.HasParent)
        {
            replacement.RemoveFromParent();
        }
            
        Insert(index, replacement);
    }
        
    [Pure]
    public TChild? SingleOfTypeOrNull<TChild>()
        where TChild : TNode
    {
        var children = OfType<TChild>();
        if (children.Count > 1)
        {
            throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 0 or 1 children of type {typeof(TChild).Name} but found {children.Count}.");
        }

        return children.Count == 1 ? children[0] : null;
    }
        
    [Pure]
    public TChild SingleOfType<TChild>()
        where TChild : TNode
    {
        var children = OfType<TChild>();
        if (children.Count != 1)
        {
            throw new InvalidOperationException($"Expected {parent.GetType().Name} to have a single child of type {typeof(TChild).Name} but found {children.Count}.");
        }

        return children[0];
    }
        
    [Pure]
    public TChild FirstOfType<TChild>()
        where TChild : TNode
    {
        var firstChild = nodes.OfType<TChild>().FirstOrDefault();
            
        return firstChild ?? throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 1 or more children of type {typeof(TChild).Name} but found none.");
    }
        
    [Pure]
    public IReadOnlyList<TChild> AllExceptFirstOfType<TChild>()
        where TChild : TNode
    {
        var children = OfType<TChild>();
        if (children.Count == 0)
        {
            throw new InvalidOperationException($"Expected {parent.GetType().Name} to have 1 or more children of type {typeof(TChild).Name} but found none.");
        }

        return children.Skip(1).ToList();
    }
        
    // TODO: Probably need some caching of these lists at some point to avoid repeated list creation.
    [Pure]
    public IReadOnlyList<TChild> OfType<TChild>()
        where TChild : TNode
    {
        return nodes.OfType<TChild>().ToList();
    }
        
    [Pure]
    public IReadOnlyList<TNode> OfType(params TType[] types) => OfType((IEnumerable<TType>) types);

    [Pure]
    public IReadOnlyList<TNode> OfType([InstantHandle] IEnumerable<TType> types)
    {
        var set = new HashSet<TType>(types);
            
        return nodes.Where(node => !set.Contains(node.NodeType)).ToList();
    }

    [Pure]
    public IReadOnlyList<TNode> ExceptOfType(params TType[] types) => ExceptOfType((IEnumerable<TType>) types);
        
    [Pure]
    public IReadOnlyList<TNode> ExceptOfType([InstantHandle] IEnumerable<TType> types)
    {
        var set = new HashSet<TType>(types);
            
        return nodes.Where(node => !set.Contains(node.NodeType)).ToList();
    }
}