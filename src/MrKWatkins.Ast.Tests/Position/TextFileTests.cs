using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class TextFileTests : FileTextFixture
{
    [Test]
    public void Constructor_FileInfo() => WithTempFile(tempFile =>
    {
        const string text = "Some Text";
        
        File.WriteAllText(tempFile.FullName, text);

        var textFile = new TextFile(tempFile);
        textFile.Name.Should().Be(tempFile.FullName);
        textFile.Text.Should().Be(text);
        textFile.Length.Should().Be(text.Length);
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
        textFile.Name.Should().Be("Test Filename");
        textFile.Text.Should().Be(text);
        textFile.Length.Should().Be(text.Length);
    }
    
    [Test]
    public void Constructor_IReadOnlyList()
    {
        const string text = "Some Text";
        
        var textFile = new TextFile("Test Filename", text);
        textFile.Name.Should().Be("Test Filename");
        textFile.Text.Should().Be(text);
        textFile.Length.Should().Be(text.Length);
    }

    [Test]
    public void CreatePosition()
    {
        const string text = "Some Text\nSome More Text";
        
        var textFile = new TextFile("Test Filename", text);

        var position = textFile.CreatePosition(15, 4, 1, 5);
        position.File.Should().BeSameAs(textFile);
        position.StartIndex.Should().Be(15);
        position.Length.Should().Be(4);
        position.StartLineIndex.Should().Be(1);
        position.StartLineNumber.Should().Be(2);
        position.StartColumnIndex.Should().Be(5);
        position.StartColumnNumber.Should().Be(6);
        position.StartLine.Should().Be("Some More Text");
        position.Text.Should().Be("More");
    }
}