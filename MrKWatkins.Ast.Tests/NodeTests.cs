namespace MrKWatkins.Ast.Tests;

public sealed class NodeTests
{
    [Test]
    public void Constructor_IEnumerable()
    {
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent).Should().Throw<InvalidOperationException>();

        root.Children.Should().BeEquivalentTo(children, options => options.WithStrictOrdering());
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

    [Test]
    public void Constructor_Array()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();

        root.Children.Should().BeEquivalentTo(children, options => options.WithStrictOrdering());
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

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
    public void Constructor_CannotAddNodesThatAlreadyHaveParents()
    {
        var root = new ANode(new BNode());

        FluentActions.Invoking(() => new ANode(root.Children[0]))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
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
    public void Ancestors_ReturnsEmptyForTheRootnode()
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
}