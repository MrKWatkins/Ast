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

        child.Parent.Should().BeSameAs(parent);
    }

    [Test]
    public void Add_Node_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        new ANode().Children.Invoking(c => c.Add(child))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
    }

    [Test]
    public void Add_Node_ThrowsIfNodeIsParent()
    {
        var node = new ANode();

        node.Children.Invoking(c => c.Add(node))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A node cannot be a child of itself.");
    }

    [Test]
    public void Add_IEnumerable_SetsParentOnChildren()
    {
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), new CNode(), new ANode() };

        var parent = new BNode();
        parent.Children.Add(children.Select(x => x));

        children.Should().OnlyContain(child => child.Parent == parent);
    }

    [Test]
    public void Add_IEnumerable_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        _ = new BNode(child);

        IEnumerable<TestNode> children = new TestNode[] { new ANode(), child, new ANode() };

        new ANode().Children.Invoking(c => c.Add(children.Select(x => x)))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
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
            .WithMessage("Node is already the child of another node.");
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
            .WithMessage("Node is already the child of another node.");
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
            .WithMessage("Node is already the child of another node.");
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

        target.Should().BeEquivalentTo(new[] { null, node.Children[0], node.Children[1], node.Children[2], null }, c => c.WithoutStrictOrdering());
    }

    [Test]
    public void Remove_RemovesParentFromChildIfRemoved()
    {
        var children = new TestNode[] { new ANode(), new CNode(), new ANode() };

        var parent = new BNode(children);

        parent.Children.Remove(children[1]).Should().BeTrue();

        children[1].HasParent.Should().BeFalse();

        parent.Children.Should().BeEquivalentTo(new[] { children[0], children[2] }, c => c.WithoutStrictOrdering());
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
            .WithMessage("node is already in this collection.");
    }

    [Test]
    public void Move_Node_DoesNotHaveParent()
    {
        var child = new BNode();

        var newParent = new CNode();
        newParent.Children.Move(child);

        newParent.Children.Should().BeEquivalentTo(new[] { child });
        child.Parent.Should().BeSameAs(newParent);
    }

    [Test]
    public void Move_Node_HasParent()
    {
        var child = new BNode();
        var parent = new ANode(child);

        var newParent = new CNode();
        newParent.Children.Move(child);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().BeEquivalentTo(new[] { child });
        child.Parent.Should().BeSameAs(newParent);
    }

    [Test]
    public void Move_IEnumerable_ThrowsIfAnyNodeAlreadyInChildren()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.Invoking(c => c.Move((IEnumerable<BNode>) new[] { new BNode(), child }))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("node is already in this collection.");
    }

    [Test]
    public void Move_IEnumerable_HasParent()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);

        var child2 = new BNode();

        IEnumerable<TestNode> children = new[] { child1, child2 };

        var newParent = new CNode();
        newParent.Children.Move(children);

        parent.Children.Should().BeEmpty();

        newParent.Children.Should().BeEquivalentTo(children);
        child1.Parent.Should().BeSameAs(newParent);
        child2.Parent.Should().BeSameAs(newParent);
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

        newParent.Children.Should().BeEquivalentTo(new[] { newParentChild, child1, child2 }, c => c.WithoutStrictOrdering());
        child1.Parent.Should().BeSameAs(newParent);
        child2.Parent.Should().BeSameAs(newParent);
    }

    [Test]
    public void Move_Params_ThrowsIfAnyNodeAlreadyInChildren()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.Invoking(c => c.Move(new BNode(), child))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("node is already in this collection.");
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

        newParent.Children.Should().BeEquivalentTo(new[] { child1, child2 }, c => c.WithoutStrictOrdering());
        child1.Parent.Should().BeSameAs(newParent);
        child2.Parent.Should().BeSameAs(newParent);
    }

    [Test]
    public void Count()
    {
        var node = new ANode(new BNode());

        node.Children.Count.Should().Be(1);

        node.Children.Add(new ANode(), new CNode());
        node.Children.Count.Should().Be(3);

        node.Children.Remove(node.Children[1]);
        node.Children.Count.Should().Be(2);

        node.Children.Clear();
        node.Children.Count.Should().Be(0);
    }

    [Test]
    public void Capacity()
    {
        var node = new ANode();

        node.Children.Capacity.Should().Be(0);

        node.Children.Add(new ANode());
        node.Children.Capacity.Should().Be(4);

        node.Children.Add(new ANode(), new ANode(), new ANode());
        node.Children.Capacity.Should().Be(4);

        node.Children.Add(new ANode());
        node.Children.Capacity.Should().Be(8);

        // Can enlarge capacity.
        node.Children.Capacity = 9;
        node.Children.Capacity.Should().Be(9);

        // Can reduce too.
        node.Children.Capacity = 5;
        node.Children.Capacity.Should().Be(5);

        // Setting to the same does nothing.
        node.Children.Capacity = 5;
        node.Children.Capacity.Should().Be(5);

        // Cannot reduce below count.
        node.Children.Invoking(c => c.Capacity = 4).Should().Throw<ArgumentOutOfRangeException>();

        node.Children.Clear();
        node.Children.Capacity.Should().Be(5);

        // Can reduce to 0.
        node.Children.Capacity = 0;
        node.Children.Capacity.Should().Be(0);
    }

    [Test]
    public void IsReadOnly() => ((IList<TestNode>) new ANode().Children).IsReadOnly.Should().BeFalse();

    [Test]
    public void IndexOf()
    {
        var child1 = new BNode();
        var child2 = new BNode();

        var parent = new ANode(child1, child2);

        parent.Children.IndexOf(child1).Should().Be(0);
        parent.Children.IndexOf(child2).Should().Be(1);
        parent.Children.IndexOf(new BNode()).Should().Be(-1);
    }

    [Test]
    public void Insert()
    {
        var child1 = new BNode { Name = "Child 1" };
        var child2 = new BNode { Name = "Child 2" };

        var parent = new ANode(child1, child2);

        var child3 = new BNode { Name = "Child 3" };
        parent.Children.Insert(1, child3);

        child3.Parent.Should().BeSameAs(parent);
        parent.Children.Should().BeEquivalentTo(new[] { child1, child3, child2 }, c => c.WithoutStrictOrdering());
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

        parent.Children.RemoveAt(1).Should().BeSameAs(child2);

        child2.HasParent.Should().BeFalse();
        parent.Children.Should().BeEquivalentTo(new[] { child1, child3 }, c => c.WithoutStrictOrdering());

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
        parent.Children.Should().BeEquivalentTo(new[] { child1, child3 }, c => c.WithoutStrictOrdering());
    }

    [Test]
    public void Indexer()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var child3 = new BNode();

        var parent = new ANode(child1, child2, child3);
        parent.Children[0].Should().BeSameAs(child1);
        parent.Children[1].Should().BeSameAs(child2);
        parent.Children[2].Should().BeSameAs(child3);

        var new2 = new BNode();

        parent.Children[1] = new2;

        parent.Children[0].Should().BeSameAs(child1);
        parent.Children[1].Should().BeSameAs(new2);
        parent.Children[2].Should().BeSameAs(child3);

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeSameAs(parent);
    }

    [Test]
    public void Indexer_Set_ThrowsIfAlreadyInCollection()
    {
        var child1 = new BNode();
        var child2 = new BNode();
        var parent = new ANode(child1, child2);

        parent.Invoking(p => p.Children[0] = child2)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already in the collection.");
    }

    [Test]
    public void Indexer_Set_ThrowsIfNodeHasParent()
    {
        var child = new BNode();
        _ = new ANode(child);

        var newParent = new BNode(new ANode());

        newParent.Invoking(p => p.Children[0] = child)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
    }

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

        parent.Children.Should().BeEquivalentTo(new[] { child1, new2, child3 }, c => c.WithStrictOrdering());

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeSameAs(parent);
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

        parent.Children.Should().BeEquivalentTo(new[] { child1, new2, child3 }, c => c.WithStrictOrdering());

        child2.HasParent.Should().BeFalse();
        new2.Parent.Should().BeSameAs(parent);
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
            .Should().Throw<ArgumentException>()
            .WithParameters("Value is already in the collection.", "replacement");
    }

    [Test]
    public void OfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.OfType<ANode>().Should().BeEquivalentTo(new[] { a });
        parent.Children.OfType<BNode>().Should().BeEquivalentTo(new[] { b1, b2 }, c => c.WithStrictOrdering());
        parent.Children.OfType<CNode>().Should().BeEmpty();
    }

    [Test]
    public void ExceptOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.ExceptOfType<ANode>().Should().BeEquivalentTo(new[] { b1, b2 }, c => c.WithStrictOrdering());
        parent.Children.ExceptOfType<BNode>().Should().BeEquivalentTo(new[] { a });
        parent.Children.ExceptOfType<CNode>().Should().BeEquivalentTo(new TestNode[] { b1, a, b2 }, c => c.WithStrictOrdering());
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
        parent.Children.FirstIfTypeOrDefault(@default).Should().BeSameAs(@default);

        parent.Children.Add(b1, a, b2);
        parent.Children.FirstIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.FirstIfTypeOrDefault<BNode>().Should().BeSameAs(b1);
        parent.Children.FirstIfTypeOrDefault(@default).Should().BeSameAs(@default);
    }

    [Test]
    public void FirstOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.FirstOfTypeOrDefault<ANode>().Should().BeSameAs(a);
        parent.Children.FirstOfTypeOrDefault<BNode>().Should().BeSameAs(b1);
        parent.Children.FirstOfTypeOrDefault(@default).Should().BeSameAs(@default);
    }

    [Test]
    public void FirstOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.FirstOfType<ANode>().Should().BeSameAs(a);
        parent.Children.FirstOfType<BNode>().Should().BeSameAs(b1);
        parent.Children.Invoking(c => c.FirstOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Expected ANode to have a child of type CNode but found none.");
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
        parent.Children.LastIfTypeOrDefault(@default).Should().BeSameAs(@default);

        parent.Children.Add(b1, a, b2);

        parent.Children.LastIfTypeOrDefault<ANode>().Should().BeNull();
        parent.Children.LastIfTypeOrDefault<BNode>().Should().BeSameAs(b2);
        parent.Children.LastIfTypeOrDefault(@default).Should().BeSameAs(@default);
    }

    [Test]
    public void LastOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.LastOfTypeOrDefault<ANode>().Should().BeSameAs(a);
        parent.Children.LastOfTypeOrDefault<BNode>().Should().BeSameAs(b2);
        parent.Children.LastOfTypeOrDefault(@default).Should().BeSameAs(@default);
    }

    [Test]
    public void LastOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.LastOfType<ANode>().Should().BeSameAs(a);
        parent.Children.LastOfType<BNode>().Should().BeSameAs(b2);
        parent.Children.Invoking(c => c.LastOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Expected ANode to have a child of type CNode but found none.");
    }

    [Test]
    public void SingleOfTypeOrDefault()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        var @default = new CNode();

        parent.Children.SingleOfTypeOrDefault<ANode>().Should().BeSameAs(a);
        parent.Children.Invoking(c => c.SingleOfTypeOrDefault<BNode>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Expected ANode to have 0 or 1 children of type BNode but found multiple.");
        parent.Children.SingleOfTypeOrDefault(@default).Should().BeSameAs(@default);
    }

    [Test]
    public void SingleOfType()
    {
        var a = new ANode();
        var b1 = new BNode();
        var b2 = new BNode();

        var parent = new ANode(b1, a, b2);

        parent.Children.SingleOfType<ANode>().Should().BeSameAs(a);
        parent.Children.Invoking(c => c.SingleOfType<BNode>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Expected ANode to have 1 child of type BNode but found multiple.");
        parent.Children.Invoking(c => c.SingleOfType<CNode>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Expected ANode to have 1 child of type CNode but found none.");
    }

    [Test]
    public void First()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.First.Should().BeSameAs(child);

        parent.Children.Add(new BNode());
        parent.Children.First.Should().BeSameAs(child);
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
        parent.Children.FirstOrNull.Should().BeSameAs(child);

        parent.Children.Add(new BNode());
        parent.Children.FirstOrNull.Should().BeSameAs(child);
    }

    [Test]
    public void UnsafeFirst()
    {
        var child = new BNode();
        var parent = new ANode(child);
        parent.Children.UnsafeFirst.Should().BeSameAs(child);

        parent.Children.Add(new BNode());
        parent.Children.UnsafeFirst.Should().BeSameAs(child);
    }

    [Test]
    public void Last()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);
        parent.Children.Last.Should().BeSameAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.Last.Should().BeSameAs(child2);
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
        parent.Children.LastOrNull.Should().BeSameAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.LastOrNull.Should().BeSameAs(child2);
    }

    [Test]
    public void UnsafeLast()
    {
        var child1 = new BNode();
        var parent = new ANode(child1);
        parent.Children.UnsafeLast.Should().BeSameAs(child1);

        var child2 = new BNode();
        parent.Children.Add(child2);
        parent.Children.UnsafeLast.Should().BeSameAs(child2);
    }

    [Test]
    public void UnsafeGet()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };
        var parent = new ANode(children);
        parent.Children.UnsafeGet(0).Should().BeSameAs(children[0]);
        parent.Children.UnsafeGet(1).Should().BeSameAs(children[1]);
        parent.Children.UnsafeGet(2).Should().BeSameAs(children[2]);
    }
}