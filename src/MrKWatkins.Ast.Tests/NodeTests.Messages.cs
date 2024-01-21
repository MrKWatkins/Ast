namespace MrKWatkins.Ast.Tests;

public sealed partial class NodeTests
{
    [Test]
    public void AddMessage()
    {
        var node = new ANode();
        node.HasMessages.Should().BeFalse();
        node.Messages.Should().BeEmpty();

        node.AddMessage(new Message(MessageLevel.Info, "First Message"));
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(
            new[]
            {
                new Message(MessageLevel.Info, "First Message")
            });

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(
            new[]
            {
                new Message(MessageLevel.Info, "First Message"),
                new Message(MessageLevel.Error, "M2", "Second Message")
            });

        node.AddMessage(MessageLevel.Warning, "M3", "Third Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(
            new[]
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
        node.Errors.Should().BeEquivalentTo(
            new[]
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
        node.Warnings.Should().BeEquivalentTo(
            new[]
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
        node.Infos.Should().BeEquivalentTo(
            new[]
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
}