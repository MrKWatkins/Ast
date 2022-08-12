namespace MrKWatkins.Ast.Tests;

public sealed class NodeListTests
{
    [Test]
    public void Add_Node_ThrowsForNullNode()
    {
        var node = new ANode();
        ANode child = null!;

        node.Invoking(n => n.Children.Add(child)).Should().Throw<ArgumentNullException>();
    }
        
    [Test]
    public void Add_Node_SetsParentOnChild()
    {
        var child = new ANode();
            
        var parent = new BNode();
        parent.Children.Add(child);

        child.Parent.Should().Be(parent);
    }
        
    [Test]
    public void Add_Node_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        var _ = new BNode(child);

        new ANode().Invoking(n => n.Children.Add(child))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
    }
        
    [Test]
    public void Add_Node_ThrowsIfNodeIsParent()
    {
        var node = new ANode();

        node.Invoking(n => n.Children.Add(node))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("A node cannot be a child of itself.");
    }
        
    [Test]
    public void Add_IEnumerable_ThrowsForNullNode()
    {
        var node = new ANode();
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), null!, new ANode() };
            
        node.Invoking(n => n.Children.Add(children)).Should().Throw<ArgumentNullException>();
    }
        
    [Test]
    public void Add_IEnumerable_SetsParentOnChildren()
    {
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), new CNode(), new ANode() };
            
        var parent = new BNode();
        parent.Children.Add(children);

        children.Should().OnlyContain(child => child.Parent == parent);
    }
        
    [Test]
    public void Add_IEnumerable_ThrowsIfNodeAlreadyHasParent()
    {
        var child = new ANode();
        var _ = new BNode(child);
            
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), child, new ANode() };

        new ANode().Invoking(n => n.Children.Add(children))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
    }
        
    [Test]
    public void Add_Array_ThrowsForNullNode() =>
        new ANode().Invoking(n => n.Children.Add(new ANode(), null!, new ANode())).Should().Throw<ArgumentNullException>();
        
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
        var _ = new BNode(child);
            
        new ANode().Invoking(n => n.Children.Add(new ANode(), child, new ANode()))
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
    public void Contains_ThrowsForNullNode() => 
        new BNode().Invoking(n => n.Children.Contains(null!)).Should().Throw<ArgumentNullException>();
        
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

        target.Should().BeEquivalentTo(new [] { null, node.Children[0], node.Children[1], node.Children[2], null });
    }
        
    [Test]
    public void Remove_ThrowsForNullNode() =>
        new ANode().Invoking(n => n.Children.Remove(null!)).Should().Throw<ArgumentNullException>();
        
    [Test]
    public void Remove_RemovesParentFromChildIfRemoved()
    {
        var children = new TestNode[] { new ANode(), new CNode(), new ANode() };
            
        var parent = new BNode(children);

        parent.Children.Remove(children[1]).Should().BeTrue();
            
        children[1].HasParent.Should().BeFalse();

        parent.Children.Should().BeEquivalentTo(new [] { children[0], children[2] });
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
    public void IsReadOnly_ReturnsFalse() => ((IList<TestNode>) new ANode().Children).IsReadOnly.Should().BeFalse();
}