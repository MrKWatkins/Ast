using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests;

public sealed class MessageFormatterTests
{
    [Test]
    public void FormatErrors()
    {
        var grandchild = new CNode { SourcePosition = new TextFilePosition(new TextFile("Test File", "Some Test Text"), 5, 4, 0, 5) };
        var child = new BNode(grandchild);
        var parent = new ANode(child);

        parent.AddError("Parent Error 1");
        parent.AddError("Parent Error 2");
        child.AddWarning("Child Warning");
        grandchild.AddError("Grandchild Error");

        MessageFormatter.FormatErrors(parent).Should().BeEquivalentTo(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            $"Test File (1, 6): Error: Grandchild Error{Environment.NewLine}Some Test Text{Environment.NewLine}     ----");

        MessageFormatter.FormatErrors(parent, false).Should().BeEquivalentTo(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            "Test File (1, 6): Error: Grandchild Error");
    }

    [Test]
    public void Format_Level()
    {
        var grandchild = new CNode { SourcePosition = new BinaryFilePosition(new BinaryFile("Non-Text File", new byte[] { 1, 2, 3 }), 1, 1) };
        var child = new BNode(grandchild);
        var parent = new ANode(child) { SourcePosition = new TextFilePosition(new TextFile("Test File", "Some Test Text"), 5, 4, 0, 5) };

        parent.AddWarning("Parent Warning");
        parent.AddError("Parent Error");
        child.AddInfo("Child Info");
        grandchild.AddWarning("Grandchild Warning 1");
        grandchild.AddWarning("Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning).Should().BeEquivalentTo(
            $"Test File (1, 6): Warning: Parent Warning{Environment.NewLine}Some Test Text{Environment.NewLine}     ----",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning, false).Should().BeEquivalentTo(
            "Test File (1, 6): Warning: Parent Warning",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
    }

    [Test]
    public void Format()
    {
        var grandchild = new CNode { SourcePosition = new BinaryFilePosition(new BinaryFile("Non-Text File", new byte[] { 1, 2, 3 }), 1, 1) };
        var child = new BNode(grandchild);
        var parent = new ANode(child) { SourcePosition = new TextFilePosition(new TextFile("Test File", "Some Test Text"), 5, 4, 0, 5) };

        parent.AddWarning("Parent Warning");
        parent.AddError("Parent Error");
        child.AddInfo("Child Info");
        grandchild.AddWarning("Grandchild Warning 1");
        grandchild.AddWarning("Grandchild Warning 2");

        var messagesByLevel = MessageFormatter.Format(parent).ToList();
        messagesByLevel.Should().HaveCount(3);
        messagesByLevel[0].Key.Should().Be(MessageLevel.Error);
        messagesByLevel[0].Should().BeEquivalentTo(
            $"Test File (1, 6): Error: Parent Error{Environment.NewLine}Some Test Text{Environment.NewLine}     ----");
        messagesByLevel[1].Key.Should().Be(MessageLevel.Warning);
        messagesByLevel[1].Should().BeEquivalentTo(
            $"Test File (1, 6): Warning: Parent Warning{Environment.NewLine}Some Test Text{Environment.NewLine}     ----",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
        messagesByLevel[2].Key.Should().Be(MessageLevel.Info);
        messagesByLevel[2].Should().BeEquivalentTo(
            "Info: Child Info");

        messagesByLevel = MessageFormatter.Format(parent, false).ToList();
        messagesByLevel.Should().HaveCount(3);
        messagesByLevel[0].Key.Should().Be(MessageLevel.Error);
        messagesByLevel[0].Should().BeEquivalentTo(
            "Test File (1, 6): Error: Parent Error");
        messagesByLevel[1].Key.Should().Be(MessageLevel.Warning);
        messagesByLevel[1].Should().BeEquivalentTo(
            "Test File (1, 6): Warning: Parent Warning",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
        messagesByLevel[2].Key.Should().Be(MessageLevel.Info);
        messagesByLevel[2].Should().BeEquivalentTo(
            "Info: Child Info");
    }
}