namespace MrKWatkins.Ast.Tests;

public sealed partial class NodeTests
{
    [Test]
    public void Parent_ThrowsForRootNode()
    {
        var root = new ANode();
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node has no parent.");
    }

    [Test]
    public void RemoveFromParent()
    {
        var child = new ANode();
        var parent = new BNode(child);

        child.RemoveFromParent();
        child.HasParent.Should().BeFalse();

        parent.Children.Should().BeEmpty();
    }

    [Test]
    public void MoveTo()
    {
        var child = new ANode();
        var parent = new BNode(child);

        var newParent = new BNode();

        child.MoveTo(newParent);
        child.Parent.Should().BeSameAs(newParent);

        parent.Children.Should().BeEmpty();
    }

    [Test]
    public void ReplaceWith()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var child3 = new ANode();

        var parent = new BNode(child1, child2, child3);

        var replacement = new CNode();

        child2.ReplaceWith(replacement);
        child2.HasParent.Should().BeFalse();

        parent.Children.Should().BeEquivalentTo(new TestNode[] { child1, replacement, child3 }, c => c.WithStrictOrdering());
    }

    [Test]
    public void HasChildren()
    {
        var root = new ANode();
        root.HasChildren.Should().BeFalse();

        root.Children.Add(new BNode());
        root.HasChildren.Should().BeTrue();
        
        root.Children.Clear();
        root.HasChildren.Should().BeFalse();
    }

    [Test]
    public void Ancestors()
    {
        var grandChildren0 = new TestNode[] { new ANode(), new BNode() };
        var grandChildren1 = new TestNode[] { new BNode(), new CNode() };
        var children = new TestNode[] { new ANode(grandChildren0), new BNode(grandChildren1) };

        var root = new ANode(children);

        children[0].Ancestors.Should().Equal(root);
        children[1].Ancestors.Should().Equal(root);

        grandChildren0[0].Ancestors.Should().Equal(children[0], root);
        grandChildren0[1].Ancestors.Should().Equal(children[0], root);

        grandChildren1[0].Ancestors.Should().Equal(children[1], root);
        grandChildren1[1].Ancestors.Should().Equal(children[1], root);
    }

    [Test]
    public void Ancestors_ReturnsEmptyForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.Ancestors.Should().BeEmpty();
    }

    [Test]
    public void ThisAndAncestors()
    {
        var grandChildren0 = new TestNode[] { new ANode(), new BNode() };
        var grandChildren1 = new TestNode[] { new BNode(), new CNode() };
        var children = new TestNode[] { new ANode(grandChildren0), new BNode(grandChildren1) };

        var root = new ANode(children);

        children[0].ThisAndAncestors.Should().Equal(children[0], root);
        children[1].ThisAndAncestors.Should().Equal(children[1], root);

        grandChildren0[0].ThisAndAncestors.Should().Equal(grandChildren0[0], children[0], root);
        grandChildren0[1].ThisAndAncestors.Should().Equal(grandChildren0[1], children[0], root);

        grandChildren1[0].ThisAndAncestors.Should().Equal(grandChildren1[0], children[1], root);
        grandChildren1[1].ThisAndAncestors.Should().Equal(grandChildren1[1], children[1], root);
    }

    [Test]
    public void ThisAndAncestors_ReturnsRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndAncestors.Should().Equal(root);
    }

    [Test]
    public void Root()
    {
        var grandChild = new CNode();
        var child1 = new BNode(grandChild);
        var child2 = new BNode();
        var root = new ANode(child1, child2);

        grandChild.Root.Should().BeSameAs(root);
        child1.Root.Should().BeSameAs(root);
        child2.Root.Should().BeSameAs(root);
        root.Root.Should().BeSameAs(root);
    }

    [Test]
    public void NextSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].NextSibling.Should().Be(children[1]);
        children[1].NextSibling.Should().Be(children[2]);
    }

    [Test]
    public void NextSibling_ReturnsNullIfNoNextSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[2].NextSibling.Should().BeNull();
    }

    [Test]
    public void NextSibling_ReturnsNullForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.NextSibling.Should().BeNull();
    }

    [Test]
    public void NextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].NextSiblings.Should().Equal(children[1], children[2]);
        children[1].NextSiblings.Should().Equal(children[2]);
    }

    [Test]
    public void NextSiblings_ReturnsEmptyIfNoNextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[2].NextSiblings.Should().BeEmpty();
    }

    [Test]
    public void NextSiblings_ReturnsEmptyForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.NextSiblings.Should().BeEmpty();
    }

    [Test]
    public void ThisAndNextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].ThisAndNextSiblings.Should().Equal(children[0], children[1], children[2]);
        children[1].ThisAndNextSiblings.Should().Equal(children[1], children[2]);
    }

    [Test]
    public void ThisAndNextSiblings_ReturnsJustThisIfNoNextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[2].ThisAndNextSiblings.Should().Equal(children[2]);
    }

    [Test]
    public void ThisAndNextSiblings_ReturnsJustTheRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndNextSiblings.Should().Equal(root);
    }

    [Test]
    public void AddNextSibling_ThrowsForRootNode()
    {
        var root = new ANode();

        root.Invoking(r => r.AddNextSibling(new ANode()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot add a next sibling to the root node.");
    }
    
    [Test]
    public void AddNextSibling_HasNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child1.AddNextSibling(sibling);
        parent.Children.Should().BeEquivalentTo(new[] { child1, sibling, child2 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void AddNextSibling_NoNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child2.AddNextSibling(sibling);
        parent.Children.Should().BeEquivalentTo(new[] { child1, child2, sibling }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void RemoveNextSibling_HasNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child1.RemoveNextSibling().Should().Be(child2);
        parent.Children.Should().BeEquivalentTo(new[] { child1 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void RemoveNextSibling_NoNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child2.RemoveNextSibling().Should().BeNull();
        parent.Children.Should().BeEquivalentTo(new[] { child1, child2 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void PreviousSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[1].PreviousSibling.Should().Be(children[0]);
        children[2].PreviousSibling.Should().Be(children[1]);
    }

    [Test]
    public void PreviousSibling_ReturnsNullIfNoPreviousSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].PreviousSibling.Should().BeNull();
    }

    [Test]
    public void PreviousSibling_ReturnsNullForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.PreviousSibling.Should().BeNull();
    }

    [Test]
    public void PreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[1].PreviousSiblings.Should().Equal(children[0]);
        children[2].PreviousSiblings.Should().Equal(children[1], children[0]);
    }

    [Test]
    public void PreviousSiblings_ReturnsEmptyIfNoPreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].PreviousSiblings.Should().BeEmpty();
    }

    [Test]
    public void PreviousSiblings_ReturnsEmptyForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.PreviousSiblings.Should().BeEmpty();
    }

    [Test]
    public void ThisAndPreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[1].ThisAndPreviousSiblings.Should().Equal(children[1], children[0]);
        children[2].ThisAndPreviousSiblings.Should().Equal(children[2], children[1], children[0]);
    }

    [Test]
    public void ThisAndPreviousSiblings_ReturnsJustThisIfNoPreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var _ = new ANode(children);

        children[0].ThisAndPreviousSiblings.Should().Equal(children[0]);
    }

    [Test]
    public void ThisAndPreviousSiblings_ReturnsJustTheRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndPreviousSiblings.Should().Equal(root);
    }

    [Test]
    public void AddPreviousSibling_ThrowsForRootNode()
    {
        var root = new ANode();

        root.Invoking(r => r.AddPreviousSibling(new ANode()))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot add a previous sibling to the root node.");
    }
    
    [Test]
    public void AddPreviousSibling_HasPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child2.AddPreviousSibling(sibling);
        parent.Children.Should().BeEquivalentTo(new[] { child1, sibling, child2 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void AddPreviousSibling_NoPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child1.AddPreviousSibling(sibling);
        parent.Children.Should().BeEquivalentTo(new[] { sibling, child1, child2 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void RemovePreviousSibling_HasPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child2.RemovePreviousSibling().Should().Be(child1);
        parent.Children.Should().BeEquivalentTo(new[] { child2 }, c => c.WithStrictOrdering());
    }
    
    [Test]
    public void RemovePreviousSibling_NoPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child1.RemovePreviousSibling().Should().BeNull();
        parent.Children.Should().BeEquivalentTo(new[] { child1, child2 }, c => c.WithStrictOrdering());
    }

    [Test]
    public void Descendents()
    {
        var grandChildren0 = new TestNode[] { new ANode(), new BNode() };
        var grandChildren1 = new TestNode[] { new BNode(), new CNode() };
        var children = new TestNode[] { new ANode(grandChildren0), new BNode(grandChildren1) };

        var root = new ANode(children);

        root.Descendents.Should().Equal(children[0], grandChildren0[0], grandChildren0[1], children[1], grandChildren1[0], grandChildren1[1]);

        children[0].Descendents.Should().Equal(grandChildren0);
        children[1].Descendents.Should().Equal(grandChildren1);

        grandChildren0[0].Descendents.Should().BeEmpty();
        grandChildren0[1].Descendents.Should().BeEmpty();
        grandChildren1[0].Descendents.Should().BeEmpty();
        grandChildren1[1].Descendents.Should().BeEmpty();
    }

    [Test]
    public void ThisAndDescendents()
    {
        var grandChildren0 = new TestNode[] { new ANode(), new BNode() };
        var grandChildren1 = new TestNode[] { new BNode(), new CNode() };
        var children = new TestNode[] { new ANode(grandChildren0), new BNode(grandChildren1) };

        var root = new ANode(children);

        root.ThisAndDescendents.Should().Equal(root, children[0], grandChildren0[0], grandChildren0[1], children[1], grandChildren1[0], grandChildren1[1]);

        children[0].ThisAndDescendents.Should().Equal(children[0], grandChildren0[0], grandChildren0[1]);
        children[1].ThisAndDescendents.Should().Equal(children[1], grandChildren1[0], grandChildren1[1]);

        grandChildren0[0].ThisAndDescendents.Should().Equal(grandChildren0[0]);
        grandChildren0[1].ThisAndDescendents.Should().Equal(grandChildren0[1]);
        grandChildren1[0].ThisAndDescendents.Should().Equal(grandChildren1[0]);
        grandChildren1[1].ThisAndDescendents.Should().Equal(grandChildren1[1]);
    }

    [Test]
    public void IndexInParent()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.IndexInParent.Should().Be(-1);
        children[0].IndexInParent.Should().Be(0);
        children[1].IndexInParent.Should().Be(1);
        children[2].IndexInParent.Should().Be(2);
    }

    [Test]
    public void IsFirstChild()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.IsFirstChild.Should().BeFalse();
        children[0].IsFirstChild.Should().BeTrue();
        children[1].IsFirstChild.Should().BeFalse();
        children[2].IsFirstChild.Should().BeFalse();
    }

    [Test]
    public void IsLastChild()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.IsLastChild.Should().BeFalse();
        children[0].IsLastChild.Should().BeFalse();
        children[1].IsLastChild.Should().BeFalse();
        children[2].IsLastChild.Should().BeTrue();
    }
    
    [Test]
    public void FirstChild()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.FirstChild.Should().BeSameAs(children[0]);
        children[0].Invoking(n => n.FirstChild).Should().Throw<InvalidOperationException>().WithMessage("Node has no children.");
    }
    
    [Test]
    public void FirstChildOrNull()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.FirstChild.Should().BeSameAs(children[0]);
        children[0].FirstChildOrNull.Should().BeNull();
    }
    
    [Test]
    public void LastChild()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.LastChild.Should().BeSameAs(children[2]);
        children[0].Invoking(n => n.LastChild).Should().Throw<InvalidOperationException>().WithMessage("Node has no children.");
    }
    
    [Test]
    public void LastChildOrNull()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.LastChild.Should().BeSameAs(children[2]);
        children[0].LastChildOrNull.Should().BeNull();
    }
}