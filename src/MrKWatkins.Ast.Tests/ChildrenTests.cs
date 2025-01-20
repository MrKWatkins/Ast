namespace MrKWatkins.Ast.Tests;

// Tests the type via the Children property of a node as the type cannot exist without a parent node.
public sealed partial class ChildrenTests
{
    [Test]
    public void Add_Node_SetsParentOnChild()
    {
        var child = new ANode();

        var parent = new BNode();
        parent.Children.Add(child);

        child.Parent.Should().BeTheSameInstanceAs(parent);
    }

    [Test]
    public void Add_Node_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        new ANode().Children.Invoking(c => c.Add(child))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Add_Node_ThrowsIfNodeIsParent()
    {
        var node = new ANode();

        node.Children.Invoking(c => c.Add(node))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("A node cannot be a child of itself.");
    }

    [Test]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public void Add_IEnumerable_SetsParentOnChildren()
    {
        IEnumerable<TestNode> children = [new ANode(), new CNode(), new ANode()];

        var parent = new BNode();
        parent.Children.Add(children.Select(x => x));

        children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Add_IEnumerable_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        IEnumerable<TestNode> children = [new ANode(), child, new ANode()];

        new ANode().Children.Invoking(c => c.Add(children.Select(x => x)))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Add_IEnumerable_ICollection_SetsParentOnChildren()
    {
        IEnumerable<TestNode> children = new List<TestNode> { new ANode(), new CNode(), new ANode() };

        var parent = new BNode();
        parent.Children.Add(children);

        children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Add_IEnumerable_IsCollection_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        IEnumerable<TestNode> children = new List<TestNode> { new ANode(), child, new ANode() };

        new ANode().Children.Invoking(c => c.Add(children))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Add_Array_SetsParentOnChildren()
    {
        var children = new TestNode[] { new ANode(), new CNode(), new ANode() };

        var parent = new BNode();
        parent.Children.Add(children[0], children[1], children[2]);

        children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Add_Array_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        new ANode().Children.Invoking(c => c.Add(new ANode(), child, new ANode()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Add_ICollection_SetsParentOnChildren()
    {
        var children = new TestNode[] { new ANode(), new CNode(), new ANode() };

        var parent = new BNode();
        parent.Children.Add(new List<TestNode> { children[0], children[1], children[2] });

        children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Add_ICollection_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        new ANode().Children.Invoking(c => c.Add(new List<TestNode> { new ANode(), child, new ANode() }))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Clear_RemovesParentFromChildren()
    {
        var children = new TestNode[] { new CNode(), new CNode(), new CNode() };

        var parent = new BNode(children);
        children.Should().OnlyContain(child => child.Parent == parent);

        parent.Children.Clear();

        parent.Children.Should().BeEmpty();
        children.Should().OnlyContain(child => !child.HasParent);

        // Use UnsafeGet to check the space at the end has been nulled out.
        parent.Children.UnsafeGet(0).Should().BeNull();
        parent.Children.UnsafeGet(1).Should().BeNull();
        parent.Children.UnsafeGet(2).Should().BeNull();
    }

    [Test]
    public void Clear_DoesNothingIfThereAreNoChildren()
    {
        var parent = new BNode();
        parent.Children.Should().BeEmpty();

        parent.Children.Clear();

        parent.Children.Should().BeEmpty();
    }

    [Test]
    public void Contains()
    {
        var node = new ANode();

        var child = new BNode();
        node.Children.Contains(child).Should().BeFalse();

        node.Children.Add(child);
        node.Children.Contains(child).Should().BeTrue();
    }

    [Test]
    public void CopyTo()
    {
        var node = new ANode(new ANode(), new BNode(), new CNode());

        var target = new TestNode[5];
        ((IList<TestNode>) node.Children).CopyTo(target, 1);

        target.Should().SequenceEqual(null, node.Children[0], node.Children[1], node.Children[2], null);
    }

    [Test]
    public void Remove_RemovesParentFromChildIfRemoved()
    {
        var children = new TestNode[] { new ANode(), new CNode(), new ANode() };

        var parent = new BNode(children);

        parent.Children.Remove(children[1]).Should().BeTrue();

        children[1].HasParent.Should().BeFalse();

        parent.Children.Should().SequenceEqual(children[0], children[2]);
        parent.Children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Remove_LeavesParentIfNotRemoved()
    {
        var parent = new BNode(new ANode(), new CNode(), new ANode());

        var otherParent = new BNode(new ANode(), new CNode());

        parent.Children.Remove(otherParent.Children[0]).Should().BeFalse();
        parent.Children.Should().HaveCount(3);
        parent.Children.Should().OnlyContain(child => child.Parent == parent);

        otherParent.Children.Should().HaveCount(2);
        otherParent.Children.Should().OnlyContain(child => child.Parent == otherParent);
    }

    [Test]
    public void Move_ThrowsIfAlreadyInChildren()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.Invoking(c => c.Move(child))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("node is already in this collection.");
    }

    [Test]
    public void Move_Node_DoesNotHaveParent()
    {
        var child = new BNode();

        var newParent = new CNode();
        newParent.Children.Move(child);

        newParent.Children.Should().SequenceEqual(child);
        child.Parent.Should().BeTheSameInstanceAs(newParent);
    }

    [Test]
    public void Move_Node_HasParent()
    {
        var child = new BNode();
        var parent = new ANode(child);

        var newParent = new CNode();
        newParent.Children.Move(child);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().SequenceEqual(child);
        child.Parent.Should().BeTheSameInstanceAs(newParent);
    }

    [Test]
    public void Move_IEnumerable_ThrowsIfAnyNodeAlreadyInChildren()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.Invoking(c => c.Move((IEnumerable<BNode>) [new BNode(), child]))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("node is already in this collection.");
    }

    [Test]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public void Move_IEnumerable_HasParent()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);

        var child2 = new BNode();

        IEnumerable<TestNode> children = [child1, child2];

        var newParent = new CNode();
        newParent.Children.Move(children);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().SequenceEqual(children);
        child1.Parent.Should().BeTheSameInstanceAs(newParent);
        child2.Parent.Should().BeTheSameInstanceAs(newParent);
    }

    [Test]
    public void Move_Children()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var parent = new ANode(child1, child2);

        var newParentChild = new BNode();
        var newParent = new BNode(newParentChild);

        // Explicit test with parent.Children as we do not want it to throw a "collection was modified" exception.
        newParent.Children.Move(parent.Children);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().SequenceEqual(newParentChild, child1, child2);
        child1.Parent.Should().BeTheSameInstanceAs(newParent);
        child2.Parent.Should().BeTheSameInstanceAs(newParent);
    }

    [Test]
    public void Move_Params_ThrowsIfAnyNodeAlreadyInChildren()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.Invoking(c => c.Move(new BNode(), child))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("node is already in this collection.");
    }

    [Test]
    public void Move_Params_HasParent()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);

        var child2 = new BNode();

        var newParent = new CNode();
        newParent.Children.Move(child1, child2);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().SequenceEqual(child1, child2);
        child1.Parent.Should().BeTheSameInstanceAs(newParent);
        child2.Parent.Should().BeTheSameInstanceAs(newParent);
    }

    [Test]
    public void Count()
    {
        var node = new ANode(new BNode());

        node.Children.Count.Should().Equal(1);

        node.Children.Add(new ANode(), new CNode());
        node.Children.Count.Should().Equal(3);

        node.Children.Remove(node.Children[1]);
        node.Children.Count.Should().Equal(2);

        node.Children.Clear();
        node.Children.Count.Should().Equal(0);
    }

    [Test]
    public void Capacity()
    {
        var node = new ANode();

        node.Children.Capacity.Should().Equal(0);

        node.Children.Add(new ANode());
        node.Children.Capacity.Should().Equal(4);

        node.Children.Add(new ANode(), new ANode(), new ANode());
        node.Children.Capacity.Should().Equal(4);

        node.Children.Add(new ANode());
        node.Children.Capacity.Should().Equal(8);

        // Can enlarge capacity.
        node.Children.Capacity = 9;
        node.Children.Capacity.Should().Equal(9);

        // Can reduce too.
        node.Children.Capacity = 5;
        node.Children.Capacity.Should().Equal(5);

        // Setting to the same does nothing.
        node.Children.Capacity = 5;
        node.Children.Capacity.Should().Equal(5);

        // Cannot reduce below count.
        node.Children.Invoking(c => c.Capacity = 4).Should().Throw<ArgumentOutOfRangeException>();

        node.Children.Clear();
        node.Children.Capacity.Should().Equal(5);

        // Can reduce to 0.
        node.Children.Capacity = 0;
        node.Children.Capacity.Should().Equal(0);
    }

    [Test]
    public void IsReadOnly() => ((IList<TestNode>) new ANode().Children).IsReadOnly.Should().BeFalse();

    [Test]
    public void IndexOf()
    {
        var child1 = new BNode();
        var child2 = new BNode();

        var parent = new ANode(child1, child2);

        parent.Children.IndexOf(child1).Should().Equal(0);
        parent.Children.IndexOf(child2).Should().Equal(1);
        parent.Children.IndexOf(new BNode()).Should().Equal(-1);
    }

    [Test]
    public void Insert()
    {
        var child1 = new BNode { Name = "Child 1" };
        var child2 = new BNode { Name = "Child 2" };

        var parent = new ANode(child1, child2);

        var child3 = new BNode { Name = "Child 3" };
        parent.Children.Insert(1, child3);

        child3.Parent.Should().BeTheSameInstanceAs(parent);
        parent.Children.Should().SequenceEqual(child1, child3, child2);
    }

    [Test]
    public void Insert_ThrowsIfOutOfRange()
    {
        var node = new ANode();
        node.Children.Invoking(c => c.Insert(1, new ANode())).Should().Throw<ArgumentOutOfRangeException>();

        node.Children.Add(new ANode());
        node.Children.Invoking(c => c.Insert(2, new ANode())).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void RemoveAt()
    {
        var child1 = new BNode { Name = "Child 1" };
        var child2 = new BNode { Name = "Child 2" };
        var child3 = new BNode { Name = "Child 3" };

        var parent = new ANode(child1, child2, child3);

        parent.Children.RemoveAt(1).Should().BeTheSameInstanceAs(child2);

        child2.HasParent.Should().BeFalse();
        parent.Children.Should().SequenceEqual(child1, child3);

        // Use UnsafeGet to check the space at the end has been nulled out.
        parent.Children.UnsafeGet(2).Should().BeNull();
    }

    [Test]
    public void RemoveAt_ThrowsIfOutOfRange()
    {
        var node = new ANode();
        node.Children.Invoking(c => c.RemoveAt(0)).Should().Throw<ArgumentOutOfRangeException>();

        node.Children.Add(new ANode());
        node.Children.Invoking(c => c.RemoveAt(1)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void RemoveAt_Explicit()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);

        ((IList<TestNode>) parent.Children).RemoveAt(1);

        child2.HasParent.Should().BeFalse();
        parent.Children.Should().SequenceEqual(child1, child3);
    }

    [Test]
    public void Indexer()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        parent.Children[0].Should().BeTheSameInstanceAs(child1);
        parent.Children[^3].Should().BeTheSameInstanceAs(child1);
        parent.Children[1].Should().BeTheSameInstanceAs(child2);
        parent.Children[^2].Should().BeTheSameInstanceAs(child2);
        parent.Children[2].Should().BeTheSameInstanceAs(child3);
        parent.Children[^1].Should().BeTheSameInstanceAs(child3);

        var new2 = new BNode();

        parent.Children[1] = new2;

        parent.Children[0].Should().BeTheSameInstanceAs(child1);
        parent.Children[1].Should().BeTheSameInstanceAs(new2);
        parent.Children[2].Should().BeTheSameInstanceAs(child3);

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeTheSameInstanceAs(parent);
    }

    [Test]
    public void Indexer_Set_ThrowsIfAlreadyInCollection()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var parent = new ANode(child1, child2);

        parent.Invoking(p => p.Children[0] = child2)
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already in the collection.");
    }

    [Test]
    public void Indexer_Set_ThrowsIfNodeHasParent()
    {
        var child = new BNode();
        _ = new ANode(child);

        var newParent = new BNode(new ANode());

        newParent.Invoking(p => p.Children[0] = child)
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void Slice()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        var slice = parent.Children[..1];
        slice.Should().SequenceEqual(child1);

        slice = parent.Children.Slice(1, 2);
        slice.Should().SequenceEqual(child2, child3);

        slice = parent.Children[..];
        slice.Should().SequenceEqual(child1, child2, child3);

        slice = parent.Children.Slice(1, 0);
        slice.Should().SequenceEqual(Array.Empty<TestNode>());
    }

    [Test]
    public void Slice_ThrowsIfIndexIsNegative() =>
        new ANode().Children.Invoking(c => _ = c.Slice(-1, 0)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void Slice_ThrowsIfCountIsNegative() =>
        // ReSharper disable once ReplaceSliceWithRangeIndexer
        new ANode().Children.Invoking(c => _ = c.Slice(0, -1)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void Slice_ThrowsIfIndexAndCountAreOutOfRange() =>
        new ANode(new BNode(), new BNode()).Children.Invoking(c => _ = c[..5]).Should().Throw<ArgumentException>();

    [Test]
    public void EnumerateSlice()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        var slice = parent.Children.EnumerateSlice(0, 1);
        slice.Should().SequenceEqual(child1);

        slice = parent.Children.EnumerateSlice(1, 2);
        slice.Should().SequenceEqual(child2, child3);

        slice = parent.Children.EnumerateSlice(0, 3);
        slice.Should().SequenceEqual(child1, child2, child3);

        slice = parent.Children.EnumerateSlice(1, 0);
        slice.Should().SequenceEqual(Array.Empty<TestNode>());
    }

    [Test]
    public void EnumerateSlice_ThrowsIfIndexIsNegative() =>
        new ANode().Children.Invoking(c => _ = c.EnumerateSlice(-1, 0)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void EnumerateSlice_ThrowsIfCountIsNegative() =>
        // ReSharper disable once ReplaceEnumerateSliceWithRangeIndexer
        new ANode().Children.Invoking(c => _ = c.EnumerateSlice(0, -1)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void EnumerateSlice_ThrowsIfIndexAndCountAreOutOfRange() =>
        new ANode(new BNode(), new BNode()).Children.Invoking(c => _ = c.EnumerateSlice(0, 5)).Should().Throw<ArgumentException>();

    [Test]
    public void EnumerateSlice_Range()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        var slice = parent.Children.EnumerateSlice(..1);
        slice.Should().SequenceEqual(child1);

        slice = parent.Children.EnumerateSlice(1..2);
        slice.Should().SequenceEqual(child2);

        slice = parent.Children.EnumerateSlice(..);
        slice.Should().SequenceEqual(child1, child2, child3);

        slice = parent.Children.EnumerateSlice(1..1);
        slice.Should().SequenceEqual(Array.Empty<TestNode>());
    }

    [Test]
    public void EnumerateSlice_Range_ThrowsIfIndexAndCountAreOutOfRange() =>
        new ANode(new BNode(), new BNode()).Children.Invoking(c => _ = c.EnumerateSlice(..5)).Should().Throw<ArgumentException>();

    [Test]
    public void UnsafeSlice()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        var slice = parent.Children.UnsafeSlice(0, 1);
        slice.ToArray().Should().SequenceEqual(child1);

        slice = parent.Children.UnsafeSlice(1, 2);
        slice.ToArray().Should().SequenceEqual(child2, child3);

        slice = parent.Children.UnsafeSlice(0, 3);
        slice.ToArray().Should().SequenceEqual(child1, child2, child3);

        slice = parent.Children.UnsafeSlice(1, 0);
        slice.ToArray().Should().SequenceEqual(Array.Empty<TestNode>());
    }

    [Test]
    public void UnsafeSlice_ThrowsIfIndexIsNegative() =>
        new ANode().Children.Invoking(c => _ = c.UnsafeSlice(-1, 0)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void UnsafeSlice_ThrowsIfCountIsNegative() =>
        // ReSharper disable once ReplaceUnsafeSliceWithRangeIndexer
        new ANode().Children.Invoking(c => _ = c.UnsafeSlice(0, -1)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void UnsafeSlice_ThrowsIfIndexAndCountAreOutOfRange() =>
        new ANode(new BNode(), new BNode()).Children.Invoking(c => _ = c.UnsafeSlice(0, 5)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void UnsafeSlice_Range()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        var slice = parent.Children.UnsafeSlice(..1);
        slice.Should().SequenceEqual(child1);

        slice = parent.Children.UnsafeSlice(1..2);
        slice.Should().SequenceEqual(child2);

        slice = parent.Children.UnsafeSlice(..3);
        slice.Should().SequenceEqual(child1, child2, child3);

        slice = parent.Children.UnsafeSlice(1..1);
        slice.Should().BeEmpty();
    }

    [Test]
    public void UnsafeSlice_Range_ThrowsIfIndexAndCountAreOutOfRange() =>
        new ANode(new BNode(), new BNode()).Children.Invoking(c => _ = c.UnsafeSlice(..5)).Should().Throw<ArgumentOutOfRangeException>();

    [Test]
    public void Replace_HasParent()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);

        var new2 = new BNode();
        var newParent = new ANode(new2);

        parent.Children.Replace(child2, new2);

        parent.Children.Should().SequenceEqual(child1, new2, child3);

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeTheSameInstanceAs(parent);
        newParent.Children.Should().BeEmpty();
    }

    [Test]
    public void Replace_NoParent()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);

        var new2 = new BNode();

        parent.Children.Replace(child2, new2);

        parent.Children.Should().SequenceEqual(child1, new2, child3);

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeTheSameInstanceAs(parent);
    }

    [Test]
    public void Replace_ThrowsIfChildCannotBeFound()
    {
        var parent = new ANode(new BNode());

        parent.Children.Invoking(c => c.Replace(new BNode(), new BNode()))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Replace_ThrowsIfAlreadyInCollection()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var parent = new ANode(child1, child2);

        parent.Invoking(p => p.Children.Replace(child1, child2))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith("Value is already in the collection.").And
            .HaveParamName("replacement");
    }

    [Test]
    public void OfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.OfType<ANode>().Should().SequenceEqual(a);
        parent.Children.OfType<BNode>().Should().SequenceEqual(b1, b2);
        parent.Children.OfType<CNode>().Should().BeEmpty();
    }

    [Test]
    public void ExceptOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.ExceptOfType<ANode>().Should().SequenceEqual(b1, b2);
        parent.Children.ExceptOfType<BNode>().Should().SequenceEqual(a);
        parent.Children.ExceptOfType<CNode>().Should().SequenceEqual(new TestNode[] { b1, a, b2 });
    }

    [Test]
    public void FirstIfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();
        var @default = new CNode();

        var parent = new ANode();

        parent.Children.FirstIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.FirstIfTypeOrDefault<BNode>().Should().BeNull();
        parent.Children.FirstIfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);

        parent.Children.Add(b1, a, b2);
        parent.Children.FirstIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.FirstIfTypeOrDefault<BNode>().Should().BeTheSameInstanceAs(b1);
        parent.Children.FirstIfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);
    }

    [Test]
    public void FirstOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.FirstOfTypeOrDefault<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.FirstOfTypeOrDefault<BNode>().Should().BeTheSameInstanceAs(b1);
        parent.Children.FirstOfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);
    }

    [Test]
    public void FirstOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.FirstOfType<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.FirstOfType<BNode>().Should().BeTheSameInstanceAs(b1);
        parent.Children.Invoking(c => c.FirstOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Expected ANode to have a child of type CNode but found none.");
    }

    [Test]
    public void LastIfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();
        var @default = new CNode();

        var parent = new ANode();

        parent.Children.LastIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.LastIfTypeOrDefault<BNode>().Should().BeNull();
        parent.Children.LastIfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);

        parent.Children.Add(b1, a, b2);

        parent.Children.LastIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.LastIfTypeOrDefault<BNode>().Should().BeTheSameInstanceAs(b2);
        parent.Children.LastIfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);
    }

    [Test]
    public void LastOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.LastOfTypeOrDefault<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.LastOfTypeOrDefault<BNode>().Should().BeTheSameInstanceAs(b2);
        parent.Children.LastOfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);
    }

    [Test]
    public void LastOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.LastOfType<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.LastOfType<BNode>().Should().BeTheSameInstanceAs(b2);
        parent.Children.Invoking(c => c.LastOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Expected ANode to have a child of type CNode but found none.");
    }

    [Test]
    public void SingleOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.SingleOfTypeOrDefault<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.Invoking(c => c.SingleOfTypeOrDefault<BNode>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Expected ANode to have 0 or 1 children of type BNode but found multiple.");
        parent.Children.SingleOfTypeOrDefault(@default).Should().BeTheSameInstanceAs(@default);
    }

    [Test]
    public void SingleOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.SingleOfType<ANode>().Should().BeTheSameInstanceAs(a);
        parent.Children.Invoking(c => c.SingleOfType<BNode>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Expected ANode to have 1 child of type BNode but found multiple.");
        parent.Children.Invoking(c => c.SingleOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Expected ANode to have 1 child of type CNode but found none.");
    }

    [Test]
    public void First()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.First.Should().BeTheSameInstanceAs(child);

        parent.Children.Add(new BNode());
        parent.Children.First.Should().BeTheSameInstanceAs(child);
    }

    [Test]
    public void First_ThrowsIfEmpty()
    {
        var parent = new ANode();
        parent.Invoking(p => p.Children.First).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void FirstOrNull()
    {
        var parent = new ANode();
        parent.Children.FirstOrNull.Should().BeNull();

        var child = new BNode();
        parent.Children.Add(child);
        parent.Children.FirstOrNull.Should().BeTheSameInstanceAs(child);

        parent.Children.Add(new BNode());
        parent.Children.FirstOrNull.Should().BeTheSameInstanceAs(child);
    }

    [Test]
    public void UnsafeFirst()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.UnsafeFirst.Should().BeTheSameInstanceAs(child);

        parent.Children.Add(new BNode());
        parent.Children.UnsafeFirst.Should().BeTheSameInstanceAs(child);
    }

    [Test]
    public void Last()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);
        parent.Children.Last.Should().BeTheSameInstanceAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.Last.Should().BeTheSameInstanceAs(child2);
    }

    [Test]
    public void Last_ThrowsIfEmpty()
    {
        var parent = new ANode();
        parent.Invoking(p => p.Children.Last).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void LastOrNull()
    {
        var parent = new ANode();
        parent.Children.LastOrNull.Should().BeNull();

        var child1 = new BNode();
        parent.Children.Add(child1);
        parent.Children.LastOrNull.Should().BeTheSameInstanceAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.LastOrNull.Should().BeTheSameInstanceAs(child2);
    }

    [Test]
    public void UnsafeLast()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);
        parent.Children.UnsafeLast.Should().BeTheSameInstanceAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.UnsafeLast.Should().BeTheSameInstanceAs(child2);
    }

    [Test]
    public void UnsafeGet()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };
        var parent = new ANode(children);
        parent.Children.UnsafeGet(0).Should().BeTheSameInstanceAs(children[0]);
        parent.Children.UnsafeGet(1).Should().BeTheSameInstanceAs(children[1]);
        parent.Children.UnsafeGet(2).Should().BeTheSameInstanceAs(children[2]);
    }
}