using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.Ast;

/// <summary>
/// Collection of child nodes for a <see cref="Node{TNode}"/>.
/// </summary>
/// <remarks>
/// Enumerating over children tries to accommodate changes to the collection whilst enumerating. However not all changes can be
/// accommodated and an <see cref="InvalidOperationException" /> will be thrown if enumeration cannot continue.
/// </remarks>
/// <typeparam name="TNode">The base node type for the collection.</typeparam>
public sealed partial class Children<TNode> : IList<TNode>
    where TNode : Node<TNode>
{
    private readonly TNode parent;
    private TNode[] nodes;

    internal Children(TNode parent)
    {
        this.parent = parent;
        nodes = Array.Empty<TNode>();
    }

    internal Children(TNode parent, [InstantHandle] IEnumerable<TNode> nodes)
    {
        this.parent = parent;
        this.nodes = nodes.ToArray();
        Count = this.nodes.Length;
        foreach (var node in this.nodes)
        {
            node.Parent = parent;
        }
    }

    /// <summary>
    /// Adds a node to the collection and assigns its <see cref="Node{TNode}.Parent" /> property.
    /// </summary>
    /// <param name="node">The node to add.</param>
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> already has a parent.</exception>
    public void Add(TNode node)
    {
        node.Parent = parent;

        var count = Count;
        if ( count >=  nodes.Length)
        {
            Grow(count + 1);
        }
        Count = count + 1;
        nodes[count] = node;
    }

    /// <summary>
    /// Adds nodes to the collection and assigns their <see cref="Node{TNode}.Parent" /> properties.
    /// </summary>
    /// <param name="nodes">The nodes to add.</param>
    /// <exception cref="InvalidOperationException">If any of <paramref name="nodes" /> already have a parent.</exception>
    // ReSharper disable once ParameterHidesMember
    public void Add([InstantHandle] IEnumerable<TNode> nodes)
    {
        if (nodes is ICollection<TNode> collection)
        {
            Add(collection);
            return;
        }

        foreach (var node in nodes)
        {
            Add(node);
        }
    }

    /// <summary>
    /// Adds nodes to the collection and assigns their <see cref="Node{TNode}.Parent" /> properties.
    /// </summary>
    /// <param name="nodes">The nodes to add.</param>
    /// <exception cref="InvalidOperationException">If any of <paramref name="nodes" /> already have a parent.</exception>
    // ReSharper disable once ParameterHidesMember
    public void Add([InstantHandle] ICollection<TNode> nodes)
    {
        var count = nodes.Count;
        if (count > 0)
        {
            foreach (var node in nodes)
            {
                node.Parent = parent;
            }

            if (this.nodes.Length - Count < count)
            {
                Grow(Count + count);
            }

            nodes.CopyTo(this.nodes, Count);
            Count += count;
        }
    }

    /// <summary>
    /// Adds nodes to the collection and assigns their <see cref="Node{TNode}.Parent" /> properties.
    /// </summary>
    /// <param name="nodes">The nodes to add.</param>
    /// <exception cref="InvalidOperationException">If any of <paramref name="nodes" /> already have a parent.</exception>
    // ReSharper disable once ParameterHidesMember
    public void Add(params TNode[] nodes) => Add((ICollection<TNode>) nodes);

    /// <summary>
    /// Removes all nodes from the collection and resets their <see cref="Node{TNode}.Parent" /> properties to <c>null</c>.
    /// </summary>
    public void Clear()
    {
        if (Count == 0)
        {
            return;
        }

        var nodesCopy = nodes;
        var count = Count;
        for (var f = 0; f < count; f++)
        {
            nodesCopy[f].RemoveParent();
        }

        Array.Clear(nodesCopy, 0, count);
        Count = 0;
    }

    /// <summary>
    /// Determines if the specified node is in the collection or not.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns><c>true</c> if <paramref name="node" /> is in the collection, <c>false</c> otherwise.</returns>
    public bool Contains(TNode node) => nodes.Contains(node);

    void ICollection<TNode>.CopyTo(TNode[] array, int arrayIndex) => Array.Copy(nodes, 0, array, arrayIndex, Count);

    /// <summary>
    /// Tries to remove a node from the collection and reset its <see cref="Node{TNode}.Parent" /> property to <c>null</c>.
    /// </summary>
    /// <param name="node">The node to remove.</param>
    /// <returns><c>true</c> if <paramref name="node" /> was in the collection and was removed, <c>false</c> otherwise.</returns>
    public bool Remove(TNode node)
    {
        var index = IndexOf(node);
        if (index != -1)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves a node from it's current parent (if it has one) and into this collection.
    /// </summary>
    /// <param name="node">The node to move.</param>
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> is already in this collection.</exception>
    public void Move(TNode node)
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
    /// Moves nodes from their current parents (if they have one) into this collection.
    /// </summary>
    /// <param name="nodes">The nodes to move.</param>
    /// <exception cref="InvalidOperationException">If <paramref name="nodes" /> contains a node that is already in this collection.</exception>
    // ReSharper disable once ParameterHidesMember
    public void Move([InstantHandle] IEnumerable<TNode> nodes)
    {
        foreach (var node in nodes)
        {
            Move(node);
        }
    }

    /// <summary>
    /// Moves nodes from their current parents (if they have one) into this collection.
    /// </summary>
    /// <param name="nodes">The nodes to move.</param>
    /// <exception cref="InvalidOperationException">If <paramref name="nodes" /> contains a node that is already in this collection.</exception>
    // ReSharper disable once ParameterHidesMember
    public void Move(params TNode[] nodes) => Move((IEnumerable<TNode>) nodes);

    /// <summary>
    /// The number of nodes in the collection.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Current capacity of the collection. The capacity is the size of the internal array used to hold items. When set, the internal
    /// array of the list is reallocated to the given capacity.
    /// </summary>
    public int Capacity
    {
        get => nodes.Length;
        set
        {
            if (value < Count)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be at least {nameof(Count)}. ({Count})");
            }

            if (value == nodes.Length)
            {
                return;
            }

            if (value > 0)
            {
                var newNodes = new TNode[value];
                if (Count > 0)
                {
                    Array.Copy(nodes, newNodes, Count);
                }
                nodes = newNodes;
            }
            else
            {
                nodes = Array.Empty<TNode>();
            }
        }
    }

    private void Grow(int capacity) => Capacity = GetNewCapacity(capacity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetNewCapacity(int capacity)
    {
        var newCapacity = nodes.Length == 0 ? 4 : 2 * nodes.Length;

        if (newCapacity < capacity)
        {
            newCapacity = capacity;
        }

        return newCapacity;
    }

    bool ICollection<TNode>.IsReadOnly => false;

    /// <summary>
    /// Gets the index of the specified node in this collection.
    /// </summary>
    /// <param name="node">The node to search for.</param>
    /// <returns>The index of the node or -1 if it is not in the collection.</returns>
    public int IndexOf(TNode node) =>
        node.HasParent && ReferenceEquals(node.Parent, parent)
            ? Array.IndexOf(nodes, node)
            : -1;

    /// <summary>
    /// Inserts a node into the collection at the specified position and assigns its <see cref="Node{TNode}.Parent" /> property.
    /// </summary>
    /// <param name="index">The position to insert the node at.</param>
    /// <param name="node">The node to insert.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is less than 0 or greater than <see cref="Count"/>.</exception>
    /// <exception cref="InvalidOperationException">If <paramref name="node" /> already has a parent.</exception>
    public void Insert(int index, TNode node)
    {
        if (index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Value must be less than or equal to {nameof(Count)}. ({Count})");
        }

        node.Parent = parent;

        if (Count == nodes.Length)
        {
            GrowForInsertion(index, 1);
        }
        else if (index < Count)
        {
            Array.Copy(nodes, index, nodes, index + 1, Count - index);
        }
        nodes[index] = node;
        Count++;
    }

    private void GrowForInsertion(int indexToInsert, int insertionCount)
    {
        var requiredCapacity = Count + insertionCount;
        var newCapacity = GetNewCapacity(requiredCapacity);

        var nodeNodes = new TNode[newCapacity];
        if (indexToInsert != 0)
        {
            Array.Copy(nodes, nodeNodes, indexToInsert);
        }

        if (Count != indexToInsert)
        {
            Array.Copy(nodes, indexToInsert, nodeNodes, indexToInsert + insertionCount, Count - indexToInsert);
        }

        nodes = nodeNodes;
    }

    void IList<TNode>.RemoveAt(int index) => RemoveAt(index);

    /// <summary>
    /// Removes the node at the specified position from the collection and reset its <see cref="Node{TNode}.Parent" /> property to <c>null</c>.
    /// </summary>
    /// <param name="index">The position of the node to remove.</param>
    /// <returns>The node that was removed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is less than 0 or equal to or greater than <see cref="Count"/>.</exception>
    public TNode RemoveAt(int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Value must be less than {nameof(Count)}. ({Count})");
        }

        var node = nodes[index];
        node.RemoveParent();
        Count--;

        // If it wasn't the last item we need to move subsequent items down one.
        if (index < Count)
        {
            Array.Copy(nodes, index + 1, nodes, index, Count - index);
        }

        // Clear the space in the array so the node can be garbage collected.
        nodes[Count] = null!;
        return node;
    }

    /// <summary>
    /// Gets or sets the node at the specified index. Parents will be updated accordingly on set.
    /// </summary>
    /// <param name="index">The position of the node to get or set.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is less than 0 or equal to or greater than <see cref="Count"/>.</exception>
    /// <exception cref="InvalidOperationException">If, when setting a node, the new node already has a parent or is already in the collection.</exception>
    public TNode this[int index]
    {
        get => nodes[index];
        set
        {
            if (value.HasParent && ReferenceEquals(value.Parent, parent))
            {
                throw new InvalidOperationException("Node is already in the collection.");
            }

            var current = nodes[index];
            value.Parent = parent;
            nodes[index] = value;
            current.RemoveParent();
        }
    }

    /// <summary>
    /// Replaces a node in the collection with another node. The replacement will be removed from its parent and the node being replaced will have it's parent removed.
    /// </summary>
    /// <param name="child">The node in the collection to replace.</param>
    /// <param name="replacement">The node to replace <paramref name="child"/> with.</param>
    /// <exception cref="ArgumentException">If <paramref name="child"/> is not in the collection or <paramref name="replacement"/> already is.</exception>
    public void Replace(TNode child, TNode replacement)
    {
        if (replacement.HasParent && ReferenceEquals(replacement.Parent, parent))
        {
            throw new ArgumentException("Value is already in the collection.", nameof(replacement));
        }

        var index = IndexOf(child);
        if (index == -1)
        {
            throw new ArgumentException("Value could not be found.", nameof(child));
        }

        var current = nodes[index];
        current.RemoveParent();

        if (replacement.HasParent)
        {
            replacement.RemoveFromParent();
        }
        replacement.Parent = parent;
        nodes[index] = replacement;
    }

    /// <summary>
    /// Lazily enumerates over all nodes in this collection of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the nodes to return.</typeparam>
    /// <returns>A lazy enumeration of all nodes in this collection of type <typeparamref name="TChild"/>.</returns>
    [Pure]
    public IEnumerable<TChild> OfType<TChild>()
        where TChild : TNode =>
        nodes.OfType<TChild>();

    /// <summary>
    /// Lazily enumerates over all nodes in this collection not of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the nodes not to return.</typeparam>
    /// <returns>A lazy enumeration of all nodes in this collection not of type <typeparamref name="TChild"/>.</returns>
    [Pure]
    public IEnumerable<TNode> ExceptOfType<TChild>()
        where TChild : TNode =>
        nodes.Where(n => n is not TChild);

    /// <summary>
    /// Returns the first node in the collection if it is of the specified type or a specified default if the collection is empty or the first node is a different type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <param name="default">The default value to return if the collection is empty or the first node is not of type <typeparamref name="TChild"/>.</param>
    /// <returns>The first node if it is of the specified type or <paramref name="default"/> otherwise.</returns>
    /// <remarks>Slightly quicker than <see cref="FirstOfTypeOrDefault{TChild}" /> if you only care about the first node.</remarks>
    [Pure]
    public TChild? FirstIfTypeOrDefault<TChild>(TChild? @default = null)
        where TChild : TNode =>
        Count != 0 ? nodes[0] as TChild ?? @default : @default;

    /// <summary>
    ///     Returns the first node in the collection of the specified type or a specified default if it doesn't contain any nodes of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <param name="default">The default value to return if the collection does not contain any nodes of type <typeparamref name="TChild" />.</param>
    /// <returns>The first node if it is of the specified type or <paramref name="default" /> if it doesn't contain any nodes of the specified type.</returns>
    [Pure]
    public TChild? FirstOfTypeOrDefault<TChild>(TChild? @default = null)
        where TChild : TNode =>
        OfType<TChild>().FirstOrDefault(@default);

    /// <summary>
    /// Returns the first node in the collection of the specified type or throws otherwise.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <returns>The first node if it is of the specified type.</returns>
    /// <exception cref="InvalidOperationException">If the collection doesn't contain any nodes of the specified type.</exception>
    [Pure]
    public TChild FirstOfType<TChild>()
        where TChild : TNode =>
        FirstOfTypeOrDefault<TChild>() ?? throw new InvalidOperationException($"Expected {parent.GetType().SimpleName()} to have a child of type {typeof(TChild).SimpleName()} but found none.");

    /// <summary>
    ///     Returns the last node in the collection if it is of the specified type or a specified default if the collection is empty or the last node is a different type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <param name="default">The default value to return if the collection is empty or the last node is not of type <typeparamref name="TChild" />.</param>
    /// <returns>The last node if it is of the specified type or <paramref name="default" /> otherwise.</returns>
    /// <remarks>Slightly quicker than <see cref="LastOfTypeOrDefault{TChild}" /> if you only care about the last node.</remarks>
    [Pure]
    public TChild? LastIfTypeOrDefault<TChild>(TChild? @default = null)
        where TChild : TNode =>
        Count != 0 ? nodes[^1] as TChild ?? @default : @default;

    /// <summary>
    /// Returns the last node in the collection of the specified type or a specified default if it doesn't contain any nodes of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <param name="default">The default value to return if the collection does not contain any nodes of type <typeparamref name="TChild"/>.</param>
    /// <returns>The last node if it is of the specified type or <paramref name="default"/> if it doesn't contain any nodes of the specified type.</returns>
    [Pure]
    public TChild? LastOfTypeOrDefault<TChild>(TChild? @default = null)
        where TChild : TNode
    {
        // Manually iterating for performance. Looks like LINQ's Reverse() doesn't optimise for IList<T>.
        for (var f = Count - 1; f >= 0; f--)
        {
            if (nodes[f] is TChild child)
            {
                return child;
            }
        }

        return @default;
    }

    /// <summary>
    /// Returns the last node in the collection of the specified type or throws otherwise.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <returns>The last node if it is of the specified type.</returns>
    /// <exception cref="InvalidOperationException">If the collection doesn't contain any nodes of the specified type.</exception>
    [Pure]
    public TChild LastOfType<TChild>()
        where TChild : TNode =>
        LastOfTypeOrDefault<TChild>() ?? throw new InvalidOperationException($"Expected {parent.GetType().SimpleName()} to have a child of type {typeof(TChild).SimpleName()} but found none.");

    /// <summary>
    /// Returns the only node in the collection of the specified type. Returns the specified default if there are no nodes in the collection of the
    /// specified type. Throws if there are multiple nodes in the collection of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <param name="default">The default value to return if the collection does not contain any nodes of type <typeparamref name="TChild"/>.</param>
    /// <returns>The single node if it is of type <typeparamref name="TChild"/> or <paramref name="default"/> if there are no nodes of type <typeparamref name="TChild"/>.</returns>
    /// <exception cref="InvalidOperationException">The collection has more than one node of type <typeparamref name="TChild"/>.</exception>
    [Pure]
    public TChild? SingleOfTypeOrDefault<TChild>(TChild? @default = null)
        where TChild : TNode
    {
        TChild? single = null;
        foreach (var child in OfType<TChild>())
        {
            if (single == null)
            {
                single = child;
            }
            else
            {
                throw new InvalidOperationException($"Expected {parent.GetType().SimpleName()} to have 0 or 1 children of type {typeof(TChild).SimpleName()} but found multiple.");
            }
        }

        return single ?? @default;
    }

    /// <summary>
    /// Returns the only node in the collection of the specified type. Throws if there is not exactly one node in the collection of the specified type.
    /// </summary>
    /// <typeparam name="TChild">The type of the node to return.</typeparam>
    /// <returns>The single node in the collection of type <typeparamref name="TChild"/>.</returns>
    /// <exception cref="InvalidOperationException">The collection has zero or more than one node of type <typeparamref name="TChild"/>.</exception>
    [Pure]
    public TChild SingleOfType<TChild>()
        where TChild : TNode
    {
        TChild? single = null;
        foreach (var child in OfType<TChild>())
        {
            if (single == null)
            {
                single = child;
            }
            else
            {
                throw new InvalidOperationException($"Expected {parent.GetType().SimpleName()} to have 1 child of type {typeof(TChild).SimpleName()} but found multiple.");
            }
        }

        return single ?? throw new InvalidOperationException($"Expected {parent.GetType().SimpleName()} to have 1 child of type {typeof(TChild).SimpleName()} but found none.");
    }

    /// <summary>
    /// Gets the first child in the collection.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the collection is empty.</exception>
    [Pure]
    public TNode First
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Children is empty.");
            }
            return this[0];
        }
    }

    /// <summary>
    /// Gets the first child in the collection or null if the collection is empty.
    /// </summary>
    [Pure]
    public TNode? FirstOrNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Count == 0 ? null : this[0];
    }

    /// <summary>
    /// Gets the first child in the collection without array bounds checks. For high performance scenarios.
    /// WARNING: Do not use unless you are certain of the number of children!
    /// </summary>
    [Pure]
    public TNode UnsafeFirst
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => MemoryMarshal.GetArrayDataReference(nodes);
    }

    /// <summary>
    /// Gets the last child in the collection.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the collection is empty.</exception>
    [Pure]
    public TNode Last
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var count = Count;
            if (count == 0)
            {
                throw new InvalidOperationException("Children is empty.");
            }
            return this[count - 1];
        }
    }

    /// <summary>
    /// Gets the last child in the collection or null if the collection is empty.
    /// </summary>
    [Pure]
    public TNode? LastOrNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var count = Count;
            return count == 0 ? null : this[count - 1];
        }
    }

    /// <summary>
    /// Gets the last child in the collection without array bounds checks. For high performance scenarios.
    /// WARNING: Do not use unless you are certain of the number of children!
    /// </summary>
    [Pure]
    public TNode UnsafeLast
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => UnsafeGet(Count - 1);
    }

    /// <summary>
    /// Gets the child at the specified index in the collection without array bounds checks. For high performance scenarios.
    /// WARNING: Do not use unless you are certain of the number of children!
    /// </summary>
    /// <param name="index">The index of the child to get.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TNode UnsafeGet(int index) => Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(nodes), index);
}