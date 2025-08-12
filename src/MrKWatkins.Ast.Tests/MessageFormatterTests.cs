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

        MessageFormatter.FormatErrors(parent).Should().SequenceEqual(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            "Error: Grandchild Error");

        MessageFormatter.FormatErrors(parent, MessageFormatterOptions.PrefixOnly).Should().SequenceEqual(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            "Test File (1, 6): Error: Grandchild Error");

        MessageFormatter.FormatErrors(parent, MessageFormatterOptions.HighlightOnly).Should().SequenceEqual(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            $"Error: Grandchild Error{Environment.NewLine}Some Test Text{Environment.NewLine}     ----");

        MessageFormatter.FormatErrors(parent, MessageFormatterOptions.PrefixAndHighlight).Should().SequenceEqual(
            "Error: Parent Error 1",
            "Error: Parent Error 2",
            $"Test File (1, 6): Error: Grandchild Error{Environment.NewLine}Some Test Text{Environment.NewLine}     ----");

    }

    [Test]
    public void Format_Level()
    {
        var grandchild = new CNode { SourcePosition = new BinaryFilePosition(new BinaryFile("Non-Text File", [1, 2, 3]), 1, 1) };
        var child = new BNode(grandchild);
        var parent = new ANode(child) { SourcePosition = new TextFilePosition(new TextFile("Test File", "Some Test Text"), 5, 4, 0, 5) };

        parent.AddWarning("Parent Warning");
        parent.AddError("Parent Error");
        child.AddInfo("Child Info");
        grandchild.AddWarning("Grandchild Warning 1");
        grandchild.AddWarning("Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning).Should().SequenceEqual(
            "Warning: Parent Warning",
            "Warning: Grandchild Warning 1",
            "Warning: Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning, MessageFormatterOptions.PrefixOnly).Should().SequenceEqual(
            "Test File (1, 6): Warning: Parent Warning",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning, MessageFormatterOptions.HighlightOnly).Should().SequenceEqual(
            $"Warning: Parent Warning{Environment.NewLine}Some Test Text{Environment.NewLine}     ----",
            "Warning: Grandchild Warning 1",
            "Warning: Grandchild Warning 2");

        MessageFormatter.Format(parent, MessageLevel.Warning, MessageFormatterOptions.PrefixAndHighlight).Should().SequenceEqual(
            $"Test File (1, 6): Warning: Parent Warning{Environment.NewLine}Some Test Text{Environment.NewLine}     ----",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
    }

    [Test]
    public void Format()
    {
        var grandchild = new CNode { SourcePosition = new BinaryFilePosition(new BinaryFile("Non-Text File", [1, 2, 3]), 1, 1) };
        var child = new BNode(grandchild);
        var parent = new ANode(child) { SourcePosition = new TextFilePosition(new TextFile("Test File", "Some Test Text"), 5, 4, 0, 5) };

        parent.AddWarning("Parent Warning");
        parent.AddError("Parent Error");
        child.AddInfo("Child Info");
        grandchild.AddWarning("Grandchild Warning 1");
        grandchild.AddWarning("Grandchild Warning 2");

        var messagesByLevel = MessageFormatter.Format(parent, MessageFormatterOptions.PrefixAndHighlight).ToList();
        messagesByLevel.Should().HaveCount(3);
        messagesByLevel[0].Key.Should().Equal(MessageLevel.Error);
        messagesByLevel[0].Should().SequenceEqual(
            $"Test File (1, 6): Error: Parent Error{Environment.NewLine}Some Test Text{Environment.NewLine}     ----");
        messagesByLevel[1].Key.Should().Equal(MessageLevel.Warning);
        messagesByLevel[1].Should().SequenceEqual(
            $"Test File (1, 6): Warning: Parent Warning{Environment.NewLine}Some Test Text{Environment.NewLine}     ----",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
        messagesByLevel[2].Key.Should().Equal(MessageLevel.Info);
        messagesByLevel[2].Should().SequenceEqual(
            "Info: Child Info");

        messagesByLevel = MessageFormatter.Format(parent, MessageFormatterOptions.PrefixOnly).ToList();
        messagesByLevel.Should().HaveCount(3);
        messagesByLevel[0].Key.Should().Equal(MessageLevel.Error);
        messagesByLevel[0].Should().SequenceEqual(
            "Test File (1, 6): Error: Parent Error");
        messagesByLevel[1].Key.Should().Equal(MessageLevel.Warning);
        messagesByLevel[1].Should().SequenceEqual(
            "Test File (1, 6): Warning: Parent Warning",
            "Non-Text File (1, 1): Warning: Grandchild Warning 1",
            "Non-Text File (1, 1): Warning: Grandchild Warning 2");
        messagesByLevel[2].Key.Should().Equal(MessageLevel.Info);
        messagesByLevel[2].Should().SequenceEqual(
            "Info: Child Info");
    }
}