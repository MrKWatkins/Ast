using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class TextFileTests : FileTextFixture
{
    [Test]
    public void Constructor_FileInfo() => WithTempFile(
        tempFile =>
        {
            const string text = "Some Text";

            File.WriteAllText(tempFile.FullName, text);

            var textFile = new TextFile(tempFile);
            textFile.Name.Should().Equal(tempFile.FullName);
            textFile.Text.Should().Equal(text);
            textFile.Length.Should().Equal(text.Length);
        });

    [Test]
    public void Constructor_Stream()
    {
        const string text = "Some Text";

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;

        var textFile = new TextFile("Test Filename", stream);
        textFile.Name.Should().Equal("Test Filename");
        textFile.Text.Should().Equal(text);
        textFile.Length.Should().Equal(text.Length);
    }

    [Test]
    public void Constructor_String()
    {
        const string text = "Some Text";

        var textFile = new TextFile("Test Filename", text);
        textFile.Name.Should().Equal("Test Filename");
        textFile.Text.Should().Equal(text);
        textFile.Length.Should().Equal(text.Length);
        textFile.Lines.Count.Should().Equal(1);
        textFile.IsEmpty.Should().BeFalse();
    }

    [Test]
    public void Constructor_Empty()
    {
        var textFile = new TextFile("Test Filename", "");
        textFile.Name.Should().Equal("Test Filename");
        textFile.Text.Should().Equal("");
        textFile.Length.Should().Equal(0);
        textFile.Lines.Count.Should().Equal(0);
        textFile.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void CreatePosition()
    {
        const string text = "Some Text\nSome More Text";

        var textFile = new TextFile("Test Filename", text);

        var position = textFile.CreatePosition(15, 4, 1, 5);
        position.File.Should().BeTheSameInstanceAs(textFile);
        position.StartIndex.Should().Equal(15);
        position.Length.Should().Equal(4);
        position.StartLineIndex.Should().Equal(1);
        position.StartLineNumber.Should().Equal(2);
        position.StartColumnIndex.Should().Equal(5);
        position.StartColumnNumber.Should().Equal(6);
        position.StartLine.Should().Equal("Some More Text");
        position.Text.Should().Equal("More");
    }

    [Test]
    public void CreateEntireFilePosition()
    {
        const string text = "Some Text\nSome More Text";

        var textFile = new TextFile("Test Filename", text);

        var position = textFile.CreateEntireFilePosition();
        position.File.Should().BeTheSameInstanceAs(textFile);
        position.StartIndex.Should().Equal(0);
        position.Length.Should().Equal(24);
        position.StartLineIndex.Should().Equal(0);
        position.StartLineNumber.Should().Equal(1);
        position.StartColumnIndex.Should().Equal(0);
        position.StartColumnNumber.Should().Equal(1);
        position.StartLine.Should().Equal("Some Text");
        position.Text.Should().Equal(text);
    }

    [Test]
    public void CreateEntireFilePosition_Empty()
    {
        var textFile = new TextFile("Test Filename", "");

        textFile.Invoking(f => f.CreateEntireFilePosition()).Should().Throw<InvalidOperationException>();
    }

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourceFile x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var file = new TextFile("Test Name", " \t Test Line 0\n   Test Line 1");

        yield return new TestCaseData(file, file, true).SetName("Reference equals");
        yield return new TestCaseData(file, new TextFile("Test Name", " \t Test Line 0\n   Test Line 1"), true).SetName("Value equals");
        yield return new TestCaseData(file, new TextFile("Another Name", " \t Test Line 0\n   Test Line 1"), false).SetName("Different name");
        yield return new TestCaseData(file, null, false).SetName("Null");
        yield return new TestCaseData(file, new BinaryFile("Test", [1, 2, 3]), false).SetName("Different SourceFile type");
        yield return new TestCaseData(file, "Different", false).SetName("Different type");
    }
}