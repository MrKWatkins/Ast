namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private TNode? parent;
    private Children<TNode>? children;
    
    /// <summary>
    /// Initialises a new instance of the <see cref="Node{TNode}" /> class with the specified children.
    /// </summary>
    /// <param name="children">The children to add.</param>
    /// <exception cref="InvalidOperationException">If any of <see cref="Children" /> already have a <see cref="Parent" />.</exception>
    protected Node([InstantHandle] IEnumerable<TNode> children)
    {
        this.children = new Children<TNode>(This, children);
    }
    
    /// <summary>
    /// The parent of this node.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// On get if the node has no parent. Use <see cref="HasParent" /> to check. On set if <paramref name="value" /> already has a parent or is this node.
    /// </exception>
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
    /// Returns <c>true</c> if this node has a <see cref="Parent" />, <c>false</c> otherwise. Nodes will not have parents if they are the
    /// root node or they have just been constructed and not yet added to a parent.
    /// </summary>
    public bool HasParent => parent != null;

    internal void RemoveParent() => parent = null;

    /// <summary>
    /// Removes this node from its <see cref="Parent" />.
    /// </summary>
    public void RemoveFromParent() => Parent.Children.Remove(This);
        
    /// <summary>
    /// The children of this node.
    /// </summary>
    public Children<TNode> Children => children ??= new Children<TNode>(This);

    /// <summary>
    /// Returns <c>true</c> if this node has any <see cref="Children" />, <c>false</c> otherwise. 
    /// </summary>
    public bool HasChildren => children is { Count: > 0 };
    
    /// <summary>
    /// Moves this node to a new parent.
    /// </summary>
    public void MoveTo(TNode newParent) => newParent.Children.MoveInto(This);

    /// <summary>
    /// Removes this node from it's parent and puts another node in its place.
    /// </summary>
    public void ReplaceWith(Node<TNode> other) => Parent.Children.Replace(This, (TNode) other);
    
    /// <summary>
    /// Lazily enumerates over this node and then the specified enumeration of nodes.
    /// </summary>
    /// <param name="and">The nodes to enumerate over after this.</param>
    /// <returns>A lazy enumeration of this node and <paramref name="and" />.</returns>
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
    
    /// <summary>
    /// Lazily enumerates over the ancestors of this node, i.e. the <see cref="Parent"/>, grandparent, great-grandparent and so on up to the root node.
    /// </summary>
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

    /// <summary>
    /// Lazily enumerates over this node and its <see cref="Ancestors" />, i.e. this node, the <see cref="Parent" />, grandparent, great-grandparent
    /// and so on up to the root node.
    /// </summary>
    public IEnumerable<TNode> ThisAndAncestors => ThisAnd(Ancestors);

    /// <summary>
    /// Lazily enumerates over this node and its <see cref="Ancestors" />, returning only ancestors of the specified type.
    /// </summary>
    /// <typeparam name="TAncestor">The type of ancestors to return.</typeparam>
    [Pure]
    public IEnumerable<TAncestor> AncestorsOfType<TAncestor>() 
        where TAncestor : TNode
        => Ancestors.OfType<TAncestor>();
    
    /// <summary>
    /// Lazily enumerates over the <see cref="Ancestors" /> of this node, returning only ancestors of the specified type.
    /// </summary>
    /// <typeparam name="TAncestor">The type of ancestors to return.</typeparam>
    [Pure]
    public IEnumerable<TAncestor> ThisAndAncestorsOfType<TAncestor>() 
        where TAncestor : TNode
        => ThisAndAncestors.OfType<TAncestor>();

    /// <summary>
    /// The root node, i.e. the highest parent above this node. Returns this node if it is the root, i.e. it has no parents.
    /// </summary>
    public TNode Root => HasParent ? Ancestors.Last() : This;

    [Pure]
    private int GetIndexOfSelf() => Parent.Children.IndexOf(This);  // Can never be -1.

    /// <summary>
    /// The next sibling, i.e. the child from the same <see cref="Parent" /> at the next positional index. Returns <c>null</c> if this node is the last child.
    /// </summary>
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

    /// <summary>
    /// Returns <c>true</c> if this node has a <see cref="NextSibling" />, <c>false</c> otherwise.
    /// </summary>
    public bool HasNextSibling => HasParent && GetIndexOfSelf() < Parent.Children.Count - 1;

    /// <summary>
    /// Lazily enumerates over the next siblings, i.e. the children from the same <see cref="Parent" /> at subsequent positional indices in ascending
    /// index order.
    /// </summary>
    public IEnumerable<TNode> NextSiblings => HasParent ? Parent.Children.Skip(GetIndexOfSelf() + 1) : Enumerable.Empty<TNode>();

    /// <summary>
    /// Lazily enumerates over this node then the next siblings, i.e. the children from the same <see cref="Parent" /> at subsequent positional indices
    /// in ascending index order.
    /// </summary>
    public IEnumerable<TNode> ThisAndNextSiblings => ThisAnd(NextSiblings);

    /// <summary>
    /// Adds the specified node as the <see cref="NextSibling" /> to this node. Existing next siblings will be moved on index to the right to accommodate.
    /// </summary>
    /// <param name="nextSibling">The node to add.</param>
    /// <exception cref="InvalidOperationException">This node is the root node or the sibling already has a <see cref="Parent" /> or it is this node.</exception>
    public void AddNextSibling(TNode nextSibling)
    {
        if (!HasParent)
        {
            throw new InvalidOperationException("Cannot add a next sibling to the root node.");
        }

        var index = GetIndexOfSelf();
        Parent.Children.Insert(index + 1, nextSibling);
    }
    
    /// <summary>
    /// Removes the <see cref="NextSibling" /> from <see cref="Parent" /> if it exists.
    /// </summary>
    /// <returns>The <see cref="NextSibling"/> removed or <c>null</c> if there was no next sibling.</returns>
    public TNode? RemoveNextSibling()
    {
        var nextSibling = NextSibling;
        nextSibling?.RemoveFromParent();
        return nextSibling;
    }
    
    /// <summary>
    /// The previous sibling, i.e. the child from the same <see cref="Parent" /> at the previous positional index. Returns <c>null</c> if this node is the first child.
    /// </summary>
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
    /// Returns <c>true</c> if this node has a <see cref="PreviousSibling" />, <c>false</c> otherwise.
    /// </summary>
    public bool HasPreviousSibling => HasParent && GetIndexOfSelf() > 0;

    /// <summary>
    /// Lazily enumerates over the previous siblings, i.e. the children from the same <see cref="Parent" /> at precedent positional indices in
    /// descending index order.
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

    /// <summary>
    /// Lazily enumerates over this node then the previous siblings, i.e. the children from the same <see cref="Parent" /> at precedent positional indices in
    /// descending index order.
    /// </summary>
    public IEnumerable<TNode> ThisAndPreviousSiblings => ThisAnd(PreviousSiblings);

    /// <summary>
    /// Adds the specified node as the <see cref="PreviousSibling" /> to this node. This and any next siblings will be moved one index to the right to accommodate.
    /// </summary>
    /// <param name="previousSibling">The node to add.</param>
    /// <exception cref="InvalidOperationException">This node is the root node or the sibling already has a <see cref="Parent" /> or it is this node.</exception>
    public void AddPreviousSibling(TNode previousSibling)
    {
        if (!HasParent)
        {
            throw new InvalidOperationException("Cannot add a previous sibling to the root node.");
        }

        var index = GetIndexOfSelf();
        Parent.Children.Insert(index, previousSibling);
    }
    
    /// <summary>
    /// Removes the <see cref="PreviousSibling" /> from <see cref="Parent" /> if it exists.
    /// </summary>
    /// <returns>The <see cref="PreviousSibling"/> removed or <c>null</c> if there was no previous sibling.</returns>
    public TNode? RemovePreviousSibling()
    {
        var previousSibling = PreviousSibling;
        previousSibling?.RemoveFromParent();
        return previousSibling;
    }

    /// <summary>
    /// Enumerates all descendents of this node in depth first pre-order.
    /// </summary>
    /// <seealso cref="MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal{TNode}" />
    public IEnumerable<TNode> Descendents => Traverse.DepthFirstPreOrder(This, false);

    /// <summary>
    /// Enumerates this node then all descendents of this node in depth first pre-order.
    /// </summary>
    /// <seealso cref="MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal{TNode}" />
    public IEnumerable<TNode> ThisAndDescendents => Traverse.DepthFirstPreOrder(This);

    /// <summary>
    /// The index of this node in the <see cref="Parent" /> or -1 if this node has no <see cref="Parent" />.
    /// </summary>
    /// <seealso cref="HasParent" />
    public int IndexInParent => HasParent ? Parent.Children.IndexOf(This) : -1;

    /// <summary>
    /// <c>true</c> if this node is the first child in <see cref="Parent" />, <c>false</c> if not or if the node has no <see cref="Parent" />.
    /// </summary>
    public bool IsFirstChild => HasParent && Parent.Children[0] == This;

    /// <summary>
    /// <c>true</c> if this node is the last child in <see cref="Parent" />, <c>false</c> if not or if the node has no <see cref="Parent" />.
    /// </summary>
    public bool IsLastChild => HasParent && Parent.Children[^1] == This;

    /// <summary>
    /// Returns the first child of this node.
    /// </summary>
    /// <exception cref="InvalidOperationException">If this node has no children.</exception>
    public TNode FirstChild => Children.Count > 0 ? Children[0] : throw new InvalidOperationException("Node has no children.");

    /// <summary>
    /// Returns the first child of this node or <c>null</c> if it has no children.
    /// </summary>
    public TNode? FirstChildOrNull => Children.Count > 0 ? Children[0] : null;

    /// <summary>
    /// Returns the last child of this node.
    /// </summary>
    /// <exception cref="InvalidOperationException">If this node has no children.</exception>
    public TNode LastChild => Children.Count > 0 ? Children[^1] : throw new InvalidOperationException("Node has no children.");

    /// <summary>
    /// Returns the last child of this node or <c>null</c> if it has no children.
    /// </summary>
    public TNode? LastChildOrNull => Children.Count > 0 ? Children[^1] : null;
}