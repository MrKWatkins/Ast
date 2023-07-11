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

    [Test]
    public void CreateEntireFilePosition()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        var binaryFile = new BinaryFile("Test Filename", bytes);

        var position = binaryFile.CreateEntireFilePosition();
        position.File.Should().BeSameAs(binaryFile);
        position.StartIndex.Should().Be(0);
        position.Length.Should().Be(5);
    }

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourceFile x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var file = new BinaryFile("Test", new byte[] { 1, 2, 3 });

        yield return new TestCaseData(file, file, true).SetName("Reference equals");
        yield return new TestCaseData(file, new BinaryFile("Test", new byte[] { 1, 2, 3 }), true).SetName("Value equals");
        yield return new TestCaseData(file, new BinaryFile("Other", new byte[] { 1, 2, 3 }), false).SetName("Different name");
        yield return new TestCaseData(file, null, false).SetName("Null");
        yield return new TestCaseData(file, new TextFile("Test Name", " \t Test Line 0\n   Test Line 1"), false).SetName("Different SourceFile type");
        yield return new TestCaseData(file, "Different", false).SetName("Different type");
    }
}