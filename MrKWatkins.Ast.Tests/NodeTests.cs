using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests;

public sealed class NodeTests
{
    [Test]
    public void Constructor()
    {
        var root = new ANode();
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent).Should().Throw<InvalidOperationException>();

        root.Children.Should().BeEmpty();
    }

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
    public void Properties()
    {
        var node = new ANode();
        node.Invoking(n => n.TestProperty).Should().Throw<KeyNotFoundException>();
        node.TestProperty = "Test";
        node.TestProperty.Should().Be("Test");
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
    public void Constructor_CannotAddNodesThatAlreadyHaveParents()
    {
        var root = new ANode(new BNode());

        FluentActions.Invoking(() => new ANode(root.Children[0]))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
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


    [Test]
    public void AddMessage()
    {
        var node = new ANode();
        node.HasMessages.Should().BeFalse();
        node.Messages.Should().BeEmpty();

        node.AddMessage(new Message(MessageLevel.Info, "First Message"));
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "First Message")
        });

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "First Message"),
            new Message(MessageLevel.Error, "M2", "Second Message")
        });

        node.AddMessage(MessageLevel.Warning, "M3", "Third Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "First Message"),
            new Message(MessageLevel.Error, "M2", "Second Message"),
            new Message(MessageLevel.Warning, "M3", "Third Message")
        });
    }

    [Test]
    public void ThisAndDescendentsHaveMessages()
    {
        var node = new ANode();
        node.ThisAndDescendentsHaveMessages.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendentsHaveMessages.Should().BeTrue();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisAndDescendentsHaveMessages.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendentsHaveMessages.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendentsHaveMessages.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendentsWithMessages()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.ThisAndDescendentsWithMessages.Should().BeEmpty();
        child.ThisAndDescendentsWithMessages.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithMessages.Should().BeEmpty();

        parent.AddError("Parent Error");
        parent.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { parent });
        child.ThisAndDescendentsWithMessages.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithMessages.Should().BeEmpty();

        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new TestNode[] { parent, grandchild });
        child.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { grandchild });
        grandchild.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { grandchild });
    }

    [Test]
    public void AddError()
    {
        var node = new ANode();
        node.HasErrors.Should().BeFalse();
        node.Errors.Should().BeEmpty();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.HasErrors.Should().BeFalse();
        node.Errors.Should().BeEmpty();

        node.AddError("M2", "Second Message");
        node.HasErrors.Should().BeTrue();
        node.Errors.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Error, "M2", "Second Message")
        });
    }

    [Test]
    public void ThisAndDescendentsHaveErrors()
    {
        var node = new ANode();
        node.ThisAndDescendentsHaveErrors.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendentsHaveErrors.Should().BeFalse();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisAndDescendentsHaveErrors.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendentsHaveErrors.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendentsHaveErrors.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new ANode(grandchild));
        grandparent.ThisAndDescendentsHaveErrors.Should().BeFalse();

        grandchild.AddError("Grandchild Error");
        grandparent.ThisAndDescendentsHaveErrors.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendentsWithErrors()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddInfo("Parent Info");
        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendentsWithErrors.Should().BeEmpty();
        child.ThisAndDescendentsWithErrors.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithErrors.Should().BeEmpty();

        parent.AddError("Parent Error");
        parent.ThisAndDescendentsWithErrors.Should().BeEquivalentTo(new[] { parent });
        child.ThisAndDescendentsWithErrors.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithErrors.Should().BeEmpty();

        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendentsWithErrors.Should().BeEquivalentTo(new TestNode[] { parent, grandchild });
        child.ThisAndDescendentsWithErrors.Should().BeEquivalentTo(new[] { grandchild });
        grandchild.ThisAndDescendentsWithErrors.Should().BeEquivalentTo(new[] { grandchild });
    }

    [Test]
    public void AddWarning()
    {
        var node = new ANode();
        node.HasWarnings.Should().BeFalse();
        node.Warnings.Should().BeEmpty();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.HasWarnings.Should().BeFalse();
        node.Warnings.Should().BeEmpty();

        node.AddWarning("M2", "Second Message");
        node.HasWarnings.Should().BeTrue();
        node.Warnings.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Warning, "M2", "Second Message")
        });
    }

    [Test]
    public void ThisAndDescendentsHaveWarnings()
    {
        var node = new ANode();
        node.ThisAndDescendentsHaveWarnings.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendentsHaveWarnings.Should().BeFalse();

        node.AddMessage(MessageLevel.Warning, "M2", "Second Message");
        node.ThisAndDescendentsHaveWarnings.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendentsHaveWarnings.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendentsHaveWarnings.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new ANode(grandchild));
        grandparent.ThisAndDescendentsHaveWarnings.Should().BeFalse();

        grandchild.AddWarning("Grandchild Warning");
        grandparent.ThisAndDescendentsHaveWarnings.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendentsWithWarnings()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddInfo("Parent Info");
        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendentsWithWarnings.Should().BeEmpty();
        child.ThisAndDescendentsWithWarnings.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithWarnings.Should().BeEmpty();

        parent.AddWarning("Parent Warning");
        parent.ThisAndDescendentsWithWarnings.Should().BeEquivalentTo(new[] { parent });
        child.ThisAndDescendentsWithWarnings.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithWarnings.Should().BeEmpty();

        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendentsWithWarnings.Should().BeEquivalentTo(new TestNode[] { parent, grandchild });
        child.ThisAndDescendentsWithWarnings.Should().BeEquivalentTo(new[] { grandchild });
        grandchild.ThisAndDescendentsWithWarnings.Should().BeEquivalentTo(new[] { grandchild });
    }

    [Test]
    public void AddInfo()
    {
        var node = new ANode();
        node.HasInfos.Should().BeFalse();
        node.Infos.Should().BeEmpty();

        node.AddMessage(MessageLevel.Error, "First Message");
        node.HasInfos.Should().BeFalse();
        node.Infos.Should().BeEmpty();

        node.AddInfo("M2", "Second Message");
        node.HasInfos.Should().BeTrue();
        node.Infos.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "M2", "Second Message")
        });
    }

    [Test]
    public void ThisAndDescendentsHaveInfos()
    {
        var node = new ANode();
        node.ThisAndDescendentsHaveInfos.Should().BeFalse();

        node.AddMessage(MessageLevel.Error, "First Message");
        node.ThisAndDescendentsHaveInfos.Should().BeFalse();

        node.AddInfo("Second Message");
        node.ThisAndDescendentsHaveInfos.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendentsHaveInfos.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendentsHaveInfos.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new BNode(grandchild));
        grandparent.ThisAndDescendentsHaveInfos.Should().BeFalse();

        grandchild.AddInfo("Grandchild Message");
        grandparent.ThisAndDescendentsHaveInfos.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendentsWithInfos()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddWarning("Parent Warning");
        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendentsWithInfos.Should().BeEmpty();
        child.ThisAndDescendentsWithInfos.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithInfos.Should().BeEmpty();

        parent.AddInfo("Parent Info");
        parent.ThisAndDescendentsWithInfos.Should().BeEquivalentTo(new[] { parent });
        child.ThisAndDescendentsWithInfos.Should().BeEmpty();
        grandchild.ThisAndDescendentsWithInfos.Should().BeEmpty();

        grandchild.AddInfo("Grandchild Info");
        parent.ThisAndDescendentsWithInfos.Should().BeEquivalentTo(new TestNode[] { parent, grandchild });
        child.ThisAndDescendentsWithInfos.Should().BeEquivalentTo(new[] { grandchild });
        grandchild.ThisAndDescendentsWithInfos.Should().BeEquivalentTo(new[] { grandchild });
    }

    [Test]
    public void Copy()
    {
        var grandchild11 = new CNode { TestProperty = "GC11" };
        var grandchild12 = new CNode { TestProperty = "GC12" };
        var child1 = new BNode(grandchild11, grandchild12) { TestProperty = "C1" };

        var grandchild21 = new CNode { TestProperty = "GC21" };
        var child2 = new BNode(grandchild21) { TestProperty = "C2" };

        var child3 = new BNode(); // Test TestProperty not set.

        var parent = new ANode(child1, child2, child3) { TestProperty = "P" };

        var copy = parent.Copy();

        copy.Should().NotBeSameAs(parent);
        copy.TestProperty.Should().Be("P");
        copy.Children.Should().HaveCount(3);

        var child1Copy = copy.Children[0];
        child1Copy.Should().NotBeSameAs(child1);
        child1Copy.TestProperty.Should().Be("C1");
        child1Copy.Children.Should().HaveCount(2);

        var grandchild11Copy = child1Copy.Children[0];
        grandchild11Copy.Should().NotBeSameAs(grandchild11);
        grandchild11Copy.TestProperty.Should().Be("GC11");
        grandchild11Copy.Children.Should().BeEmpty();

        var grandchild12Copy = child1Copy.Children[1];
        grandchild12Copy.Should().NotBeSameAs(grandchild12);
        grandchild12Copy.TestProperty.Should().Be("GC12");
        grandchild12Copy.Children.Should().BeEmpty();

        var child2Copy = copy.Children[1];
        child2Copy.Should().NotBeSameAs(child2);
        child2Copy.TestProperty.Should().Be("C2");
        child2Copy.Children.Should().HaveCount(1);

        var grandchild21Copy = child2Copy.Children[0];
        grandchild21Copy.Should().NotBeSameAs(grandchild21);
        grandchild21Copy.TestProperty.Should().Be("GC21");
        grandchild21Copy.Children.Should().BeEmpty();

        var child3Copy = copy.Children[2];
        child3Copy.Should().NotBeSameAs(child3);
        child3Copy.Invoking(c => c.TestProperty).Should().Throw<KeyNotFoundException>();
        child3Copy.Children.Should().BeEmpty();
    }

    [Test]
    public void Copy_INodeFactory()
    {
        var child = new BNode { TestProperty = "Child" };
        var parent = new ANode(child);

        var copy = parent.Copy(new CustomNodeFactory());

        copy.Should().BeOfType<CNode>();
        copy.Invoking(c => c.TestProperty).Should().Throw<KeyNotFoundException>();
        copy.Children.Should().HaveCount(1);

        var childCopy = copy.Children[0];
        childCopy.Should().BeOfType<CNode>();
        childCopy.TestProperty.Should().Be("Child");
        childCopy.Children.Should().BeEmpty();
    }

    [Test]
    public void SourcePosition()
    {
        var node = new ANode();
        node.SourcePosition.Should().BeSameAs(MrKWatkins.Ast.Position.SourcePosition.None);

        var position = new BinaryFilePosition(new BinaryFile("Test File", new byte[] { 1, 2, 3 }), 0, 1);
        node.SourcePosition = position;
        node.SourcePosition.Should().BeSameAs(position);
    }

    [Test]
    public void ToStringTest()
    {
        new ANode().ToString().Should().Be("ANode");
        new BNode().ToString().Should().Be("BNode");
        new CNode().ToString().Should().Be("CNode");
    }

    private sealed class CustomNodeFactory : INodeFactory<TestNode>
    {
        public TestNode Create(Type nodeType) => new CNode();
    }
}