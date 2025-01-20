using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class BinaryFileTests : FileTextFixture
{
    [Test]
    public void Constructor_FileInfo() => WithTempFile(
        tempFile =>
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5 };

            File.WriteAllBytes(tempFile.FullName, bytes);

            var binaryFile = new BinaryFile(tempFile);
            binaryFile.Name.Should().Equal(tempFile.FullName);
            binaryFile.Bytes.Should().SequenceEqual(bytes);
            binaryFile.Length.Should().Equal(bytes.Length);
        });

    [Test]
    public void Constructor_Stream()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        using var stream = new MemoryStream();
        stream.Write(bytes);
        stream.Position = 0;

        var binaryFile = new BinaryFile("Test Filename", stream);
        binaryFile.Name.Should().Equal("Test Filename");
        binaryFile.Bytes.Should().SequenceEqual(bytes);
        binaryFile.Length.Should().Equal(bytes.Length);
    }

    [Test]
    public void Constructor_IReadOnlyList()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        var binaryFile = new BinaryFile("Test Filename", bytes);
        binaryFile.Name.Should().Equal("Test Filename");
        binaryFile.Bytes.Should().SequenceEqual(bytes);
        binaryFile.Length.Should().Equal(bytes.Length);
    }

    [Test]
    public void CreatePosition()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        var binaryFile = new BinaryFile("Test Filename", bytes);

        var position = binaryFile.CreatePosition(1, 2);
        position.File.Should().BeTheSameInstanceAs(binaryFile);
        position.StartIndex.Should().Equal(1);
        position.Length.Should().Equal(2);
    }

    [Test]
    public void CreateEntireFilePosition()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        var binaryFile = new BinaryFile("Test Filename", bytes);

        var position = binaryFile.CreateEntireFilePosition();
        position.File.Should().BeTheSameInstanceAs(binaryFile);
        position.StartIndex.Should().Equal(0);
        position.Length.Should().Equal(5);
    }

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourceFile x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var file = new BinaryFile("Test", [1, 2, 3]);

        yield return new TestCaseData(file, file, true).SetName("Reference equals");
        yield return new TestCaseData(file, new BinaryFile("Test", [1, 2, 3]), true).SetName("Value equals");
        yield return new TestCaseData(file, new BinaryFile("Other", [1, 2, 3]), false).SetName("Different name");
        yield return new TestCaseData(file, null, false).SetName("Null");
        yield return new TestCaseData(file, new TextFile("Test Name", " \t Test Line 0\n   Test Line 1"), false).SetName("Different SourceFile type");
        yield return new TestCaseData(file, "Different", false).SetName("Different type");
    }
}