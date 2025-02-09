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
            .That.Should().HaveMessage("Node has no parent.");
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
        child.Parent.Should().BeTheSameInstanceAs(newParent);

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

        parent.Children.Should().SequenceEqual(child1, replacement, child3);
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

        children[0].Ancestors.Should().SequenceEqual(root);
        children[1].Ancestors.Should().SequenceEqual(root);

        grandChildren0[0].Ancestors.Should().SequenceEqual(children[0], root);
        grandChildren0[1].Ancestors.Should().SequenceEqual(children[0], root);

        grandChildren1[0].Ancestors.Should().SequenceEqual(children[1], root);
        grandChildren1[1].Ancestors.Should().SequenceEqual(children[1], root);
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

        children[0].ThisAndAncestors.Should().SequenceEqual(children[0], root);
        children[1].ThisAndAncestors.Should().SequenceEqual(children[1], root);

        grandChildren0[0].ThisAndAncestors.Should().SequenceEqual(grandChildren0[0], children[0], root);
        grandChildren0[1].ThisAndAncestors.Should().SequenceEqual(grandChildren0[1], children[0], root);

        grandChildren1[0].ThisAndAncestors.Should().SequenceEqual(grandChildren1[0], children[1], root);
        grandChildren1[1].ThisAndAncestors.Should().SequenceEqual(grandChildren1[1], children[1], root);
    }

    [Test]
    public void ThisAndAncestors_ReturnsRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndAncestors.Should().SequenceEqual(root);
    }

    [Test]
    public void AncestorsOfType()
    {
        var grandChild00 = new ANode();
        var grandChild01 = new BNode();
        var child0 = new ANode(grandChild00, grandChild01);

        var grandChild10 = new BNode();
        var grandChild11 = new CNode();
        var child1 = new BNode(grandChild10, grandChild11);

        var root = new ANode(child0, child1);

        child0.AncestorsOfType<ANode>().Should().SequenceEqual(root);
        child0.AncestorsOfType<BNode>().Should().BeEmpty();

        grandChild00.AncestorsOfType<ANode>().Should().SequenceEqual(child0, root);
        grandChild01.AncestorsOfType<BNode>().Should().BeEmpty();

        grandChild10.AncestorsOfType<ANode>().Should().SequenceEqual(root);
        grandChild10.AncestorsOfType<BNode>().Should().SequenceEqual(child1);
    }

    [Test]
    public void ThisAndAncestorsOfType()
    {
        var grandChild00 = new ANode();
        var grandChild01 = new BNode();
        var child0 = new ANode(grandChild00, grandChild01);

        var grandChild10 = new BNode();
        var grandChild11 = new CNode();
        var child1 = new BNode(grandChild10, grandChild11);

        var root = new ANode(child0, child1);

        child0.ThisAndAncestorsOfType<ANode>().Should().SequenceEqual(child0, root);
        child0.ThisAndAncestorsOfType<BNode>().Should().BeEmpty();

        grandChild00.ThisAndAncestorsOfType<ANode>().Should().SequenceEqual(grandChild00, child0, root);
        grandChild01.ThisAndAncestorsOfType<BNode>().Should().SequenceEqual(grandChild01);

        grandChild10.ThisAndAncestorsOfType<ANode>().Should().SequenceEqual(root);
        grandChild10.ThisAndAncestorsOfType<BNode>().Should().SequenceEqual(grandChild10, child1);
    }

    [Test]
    public void Root()
    {
        var grandChild = new CNode();
        var child1 = new BNode(grandChild);
        var child2 = new BNode();
        var root = new ANode(child1, child2);

        grandChild.Root.Should().BeTheSameInstanceAs(root);
        child1.Root.Should().BeTheSameInstanceAs(root);
        child2.Root.Should().BeTheSameInstanceAs(root);
        root.Root.Should().BeTheSameInstanceAs(root);
    }

    [Test]
    public void NextSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.NextSibling.Should().BeNull();
        children[0].NextSibling.Should().Equal(children[1]);
        children[1].NextSibling.Should().Equal(children[2]);
        children[2].NextSibling.Should().BeNull();
    }

    [Test]
    public void HasNextSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.HasNextSibling.Should().BeFalse();
        children[0].HasNextSibling.Should().BeTrue();
        children[1].HasNextSibling.Should().BeTrue();
        children[2].HasNextSibling.Should().BeFalse();
    }

    [Test]
    public void NextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

        children[0].NextSiblings.Should().SequenceEqual(children[1], children[2]);
        children[1].NextSiblings.Should().SequenceEqual(children[2]);
    }

    [Test]
    public void NextSiblings_ReturnsEmptyIfNoNextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

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

        _ = new ANode(children);

        children[0].ThisAndNextSiblings.Should().SequenceEqual(children[0], children[1], children[2]);
        children[1].ThisAndNextSiblings.Should().SequenceEqual(children[1], children[2]);
    }

    [Test]
    public void ThisAndNextSiblings_ReturnsJustThisIfNoNextSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

        children[2].ThisAndNextSiblings.Should().SequenceEqual(children[2]);
    }

    [Test]
    public void ThisAndNextSiblings_ReturnsJustTheRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndNextSiblings.Should().SequenceEqual(root);
    }

    [Test]
    public void AddNextSibling_ThrowsForRootNode()
    {
        var root = new ANode();

        root.Invoking(r => r.AddNextSibling(new ANode()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Cannot add a next sibling to the root node.");
    }

    [Test]
    public void AddNextSibling_HasNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child1.AddNextSibling(sibling);
        parent.Children.Should().SequenceEqual(child1, sibling, child2);
    }

    [Test]
    public void AddNextSibling_NoNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child2.AddNextSibling(sibling);
        parent.Children.Should().SequenceEqual(child1, child2, sibling);
    }

    [Test]
    public void RemoveNextSibling_HasNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child1.RemoveNextSibling().Should().Equal(child2);
        parent.Children.Should().SequenceEqual(child1);
    }

    [Test]
    public void RemoveNextSibling_NoNextSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child2.RemoveNextSibling().Should().BeNull();
        parent.Children.Should().SequenceEqual(child1, child2);
    }

    [Test]
    public void PreviousSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.PreviousSibling.Should().BeNull();
        children[0].PreviousSibling.Should().BeNull();
        children[1].PreviousSibling.Should().Equal(children[0]);
        children[2].PreviousSibling.Should().Equal(children[1]);
    }

    [Test]
    public void HasPreviousSibling()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.HasPreviousSibling.Should().BeFalse();
        children[0].HasPreviousSibling.Should().BeFalse();
        children[1].HasPreviousSibling.Should().BeTrue();
        children[2].HasPreviousSibling.Should().BeTrue();
    }

    [Test]
    public void PreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

        children[1].PreviousSiblings.Should().SequenceEqual(children[0]);
        children[2].PreviousSiblings.Should().SequenceEqual(children[1], children[0]);
    }

    [Test]
    public void PreviousSiblings_ReturnsEmptyIfNoPreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

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

        _ = new ANode(children);

        children[1].ThisAndPreviousSiblings.Should().SequenceEqual(children[1], children[0]);
        children[2].ThisAndPreviousSiblings.Should().SequenceEqual(children[2], children[1], children[0]);
    }

    [Test]
    public void ThisAndPreviousSiblings_ReturnsJustThisIfNoPreviousSiblings()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        _ = new ANode(children);

        children[0].ThisAndPreviousSiblings.Should().SequenceEqual(children[0]);
    }

    [Test]
    public void ThisAndPreviousSiblings_ReturnsJustTheRootForTheRootNode()
    {
        var root = new ANode(new ANode(), new BNode(), new CNode());

        root.ThisAndPreviousSiblings.Should().SequenceEqual(root);
    }

    [Test]
    public void AddPreviousSibling_ThrowsForRootNode()
    {
        var root = new ANode();

        root.Invoking(r => r.AddPreviousSibling(new ANode()))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Cannot add a previous sibling to the root node.");
    }

    [Test]
    public void AddPreviousSibling_HasPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child2.AddPreviousSibling(sibling);
        parent.Children.Should().SequenceEqual(child1, sibling, child2);
    }

    [Test]
    public void AddPreviousSibling_NoPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        var sibling = new ANode();

        child1.AddPreviousSibling(sibling);
        parent.Children.Should().SequenceEqual(sibling, child1, child2);
    }

    [Test]
    public void RemovePreviousSibling_HasPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child2.RemovePreviousSibling().Should().Equal(child1);
        parent.Children.Should().SequenceEqual(child2);
    }

    [Test]
    public void RemovePreviousSibling_NoPreviousSibling()
    {
        var child1 = new ANode();
        var child2 = new ANode();
        var parent = new ANode(child1, child2);

        child1.RemovePreviousSibling().Should().BeNull();
        parent.Children.Should().SequenceEqual(child1, child2);
    }

    [Test]
    public void Descendents()
    {
        var grandChildren0 = new TestNode[] { new ANode(), new BNode() };
        var grandChildren1 = new TestNode[] { new BNode(), new CNode() };
        var children = new TestNode[] { new ANode(grandChildren0), new BNode(grandChildren1) };

        var root = new ANode(children);

        root.Descendents.Should().SequenceEqual(children[0], grandChildren0[0], grandChildren0[1], children[1], grandChildren1[0], grandChildren1[1]);

        children[0].Descendents.Should().SequenceEqual(grandChildren0);
        children[1].Descendents.Should().SequenceEqual(grandChildren1);

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

        root.ThisAndDescendents.Should().SequenceEqual(root, children[0], grandChildren0[0], grandChildren0[1], children[1], grandChildren1[0], grandChildren1[1]);

        children[0].ThisAndDescendents.Should().SequenceEqual(children[0], grandChildren0[0], grandChildren0[1]);
        children[1].ThisAndDescendents.Should().SequenceEqual(children[1], grandChildren1[0], grandChildren1[1]);

        grandChildren0[0].ThisAndDescendents.Should().SequenceEqual(grandChildren0[0]);
        grandChildren0[1].ThisAndDescendents.Should().SequenceEqual(grandChildren0[1]);
        grandChildren1[0].ThisAndDescendents.Should().SequenceEqual(grandChildren1[0]);
        grandChildren1[1].ThisAndDescendents.Should().SequenceEqual(grandChildren1[1]);
    }

    [Test]
    public void IndexInParent()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);

        root.IndexInParent.Should().Equal(-1);
        children[0].IndexInParent.Should().Equal(0);
        children[1].IndexInParent.Should().Equal(1);
        children[2].IndexInParent.Should().Equal(2);
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
}