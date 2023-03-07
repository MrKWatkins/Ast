using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class BinaryFileTests : FileTextFixture
{
    [Test]
    public void Constructor_FileInfo() => WithTempFile(tempFile =>
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        
        File.WriteAllBytes(tempFile.FullName, bytes);

        var binaryFile = new BinaryFile(tempFile);
        binaryFile.Name.Should().Be(tempFile.FullName);
        binaryFile.Bytes.Should().BeEquivalentTo(bytes, c => c.WithStrictOrdering());
        binaryFile.Length.Should().Be(bytes.Length);
    });
    
    [Test]
    public void Constructor_Stream()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        
        using var stream = new MemoryStream();
        stream.Write(bytes);
        stream.Position = 0;

        var binaryFile = new BinaryFile("Test Filename", stream);
        binaryFile.Name.Should().Be("Test Filename");
        binaryFile.Bytes.Should().BeEquivalentTo(bytes, c => c.WithStrictOrdering());
        binaryFile.Length.Should().Be(bytes.Length);
    }
    
    [Test]
    public void Constructor_IReadOnlyList()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        
        var binaryFile = new BinaryFile("Test Filename", bytes);
        binaryFile.Name.Should().Be("Test Filename");
        binaryFile.Bytes.Should().BeEquivalentTo(bytes, c => c.WithStrictOrdering());
        binaryFile.Length.Should().Be(bytes.Length);
    }

    [Test]
    public void CreatePosition()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        
        var binaryFile = new BinaryFile("Test Filename", bytes);

        var position = binaryFile.CreatePosition(1, 2);
        position.File.Should().BeSameAs(binaryFile);
        position.StartIndex.Should().Be(1);
        position.Length.Should().Be(2);
    }
}