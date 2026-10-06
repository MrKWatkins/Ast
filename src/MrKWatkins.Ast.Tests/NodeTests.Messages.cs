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
        node.Messages.Should().SequenceEqual(new Message(MessageLevel.Info, "First Message"));

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().SequenceEqual(new Message(MessageLevel.Info, "First Message"), new Message(MessageLevel.Error, "M2", "Second Message"));

        node.AddMessage(MessageLevel.Warning, "M3", "Third Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().SequenceEqual(new Message(MessageLevel.Info, "First Message"), new Message(MessageLevel.Error, "M2", "Second Message"), new Message(MessageLevel.Warning, "M3", "Third Message"));
    }

    [Test]
    public void ThisAndDescendantsHaveMessages()
    {
        var node = new ANode();
        node.ThisAndDescendantsHaveMessages.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendantsHaveMessages.Should().BeTrue();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisAndDescendantsHaveMessages.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendantsHaveMessages.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendantsHaveMessages.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendantsWithMessages()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.ThisAndDescendantsWithMessages.Should().BeEmpty();
        child.ThisAndDescendantsWithMessages.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithMessages.Should().BeEmpty();

        parent.AddError("Parent Error");
        parent.ThisAndDescendantsWithMessages.Should().SequenceEqual(parent);
        child.ThisAndDescendantsWithMessages.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithMessages.Should().BeEmpty();

        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendantsWithMessages.Should().SequenceEqual(parent, grandchild);
        child.ThisAndDescendantsWithMessages.Should().SequenceEqual(grandchild);
        grandchild.ThisAndDescendantsWithMessages.Should().SequenceEqual(grandchild);
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
        node.Errors.Should().SequenceEqual(new Message(MessageLevel.Error, "M2", "Second Message"));
    }

    [Test]
    public void ThisAndDescendantsHaveErrors()
    {
        var node = new ANode();
        node.ThisAndDescendantsHaveErrors.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendantsHaveErrors.Should().BeFalse();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisAndDescendantsHaveErrors.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendantsHaveErrors.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendantsHaveErrors.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new ANode(grandchild));
        grandparent.ThisAndDescendantsHaveErrors.Should().BeFalse();

        grandchild.AddError("Grandchild Error");
        grandparent.ThisAndDescendantsHaveErrors.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendantsWithErrors()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddInfo("Parent Info");
        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendantsWithErrors.Should().BeEmpty();
        child.ThisAndDescendantsWithErrors.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithErrors.Should().BeEmpty();

        parent.AddError("Parent Error");
        parent.ThisAndDescendantsWithErrors.Should().SequenceEqual(parent);
        child.ThisAndDescendantsWithErrors.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithErrors.Should().BeEmpty();

        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendantsWithErrors.Should().SequenceEqual(parent, grandchild);
        child.ThisAndDescendantsWithErrors.Should().SequenceEqual(grandchild);
        grandchild.ThisAndDescendantsWithErrors.Should().SequenceEqual(grandchild);
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
        node.Warnings.Should().SequenceEqual(new Message(MessageLevel.Warning, "M2", "Second Message"));
    }

    [Test]
    public void ThisAndDescendantsHaveWarnings()
    {
        var node = new ANode();
        node.ThisAndDescendantsHaveWarnings.Should().BeFalse();

        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisAndDescendantsHaveWarnings.Should().BeFalse();

        node.AddMessage(MessageLevel.Warning, "M2", "Second Message");
        node.ThisAndDescendantsHaveWarnings.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendantsHaveWarnings.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendantsHaveWarnings.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new ANode(grandchild));
        grandparent.ThisAndDescendantsHaveWarnings.Should().BeFalse();

        grandchild.AddWarning("Grandchild Warning");
        grandparent.ThisAndDescendantsHaveWarnings.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendantsWithWarnings()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddInfo("Parent Info");
        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendantsWithWarnings.Should().BeEmpty();
        child.ThisAndDescendantsWithWarnings.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithWarnings.Should().BeEmpty();

        parent.AddWarning("Parent Warning");
        parent.ThisAndDescendantsWithWarnings.Should().SequenceEqual(parent);
        child.ThisAndDescendantsWithWarnings.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithWarnings.Should().BeEmpty();

        grandchild.AddWarning("Grandchild Warning");
        parent.ThisAndDescendantsWithWarnings.Should().SequenceEqual(parent, grandchild);
        child.ThisAndDescendantsWithWarnings.Should().SequenceEqual(grandchild);
        grandchild.ThisAndDescendantsWithWarnings.Should().SequenceEqual(grandchild);
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
        node.Infos.Should().SequenceEqual(new Message(MessageLevel.Info, "M2", "Second Message"));
    }

    [Test]
    public void ThisAndDescendantsHaveInfos()
    {
        var node = new ANode();
        node.ThisAndDescendantsHaveInfos.Should().BeFalse();

        node.AddMessage(MessageLevel.Error, "First Message");
        node.ThisAndDescendantsHaveInfos.Should().BeFalse();

        node.AddInfo("Second Message");
        node.ThisAndDescendantsHaveInfos.Should().BeTrue();

        var parent = new ANode();
        parent.ThisAndDescendantsHaveInfos.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisAndDescendantsHaveInfos.Should().BeTrue();

        var grandchild = new ANode();
        var grandparent = new ANode(new BNode(grandchild));
        grandparent.ThisAndDescendantsHaveInfos.Should().BeFalse();

        grandchild.AddInfo("Grandchild Message");
        grandparent.ThisAndDescendantsHaveInfos.Should().BeTrue();
    }

    [Test]
    public void ThisAndDescendantsWithInfos()
    {
        var grandchild = new CNode();
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddWarning("Parent Warning");
        grandchild.AddError("Grandchild Error");
        parent.ThisAndDescendantsWithInfos.Should().BeEmpty();
        child.ThisAndDescendantsWithInfos.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithInfos.Should().BeEmpty();

        parent.AddInfo("Parent Info");
        parent.ThisAndDescendantsWithInfos.Should().SequenceEqual(parent);
        child.ThisAndDescendantsWithInfos.Should().BeEmpty();
        grandchild.ThisAndDescendantsWithInfos.Should().BeEmpty();

        grandchild.AddInfo("Grandchild Info");
        parent.ThisAndDescendantsWithInfos.Should().SequenceEqual(parent, grandchild);
        child.ThisAndDescendantsWithInfos.Should().SequenceEqual(grandchild);
        grandchild.ThisAndDescendantsWithInfos.Should().SequenceEqual(grandchild);
    }
}