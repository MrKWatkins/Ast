using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class BinaryFilePositionTests : EqualityTestFixture
{
    [Test]
    public void Constructor()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };

        var file = new BinaryFile("Test Filename", bytes);

        var position = new BinaryFilePosition(file, 1, 2);
        position.File.Should().BeSameAs(file);
        position.StartIndex.Should().Be(1);
        position.Length.Should().Be(2);
    }

    [TestCase(0, 0, 0, 0, 0, 0)]
    [TestCase(0, 0, 4, 0, 0, 4)]
    [TestCase(0, 0, 4, 1, 0, 5)]
    [TestCase(0, 2, 1, 2, 0, 3)]
    [TestCase(0, 5, 2, 2, 0, 5)]
    public void CreateCombination(int startIndexX, int lengthX, int startIndexY, int lengthY, int expectedStartIndex, int expectedLength)
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var file = new BinaryFile("Test Filename", bytes);

        var positionX = new BinaryFilePosition(file, startIndexX, lengthX);
        var positionY = new BinaryFilePosition(file, startIndexY, lengthY);

        var combined = positionX.Combine(positionY);
        combined.StartIndex.Should().Be(expectedStartIndex);
        combined.Length.Should().Be(expectedLength);

        combined = positionY.Combine(positionX);
        combined.StartIndex.Should().Be(expectedStartIndex);
        combined.Length.Should().Be(expectedLength);
    }

    [Test]
    public void Addition()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var file = new BinaryFile("Test Filename", bytes);

        var positionX = new BinaryFilePosition(file, 0, 5);
        var positionY = new BinaryFilePosition(file, 2, 2);

        (positionX + positionY).Should().Be(positionX.Combine(positionY));
        (positionY + positionX).Should().Be(positionY.Combine(positionX));
    }

    [Test]
    public void CreateZeroWidthPrefix()
    {
        var file = new BinaryFile("Test Filename", new byte[] { 1, 2, 3, 4, 5 });
        var position = new BinaryFilePosition(file, 2, 3);

        var zeroWidth = position.CreateZeroWidthPrefix();

        zeroWidth.Should().Be(new BinaryFilePosition(file, 2, 0));
    }

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourcePosition x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var binaryFile = new BinaryFile("Test", new byte[] { 1, 2, 3 });
        var position = new BinaryFilePosition(binaryFile, 0, 1);

        yield return new TestCaseData(position, position, true).SetName("Reference equals");
        yield return new TestCaseData(position, new BinaryFilePosition(binaryFile, 0, 1), true).SetName("Value equals");
        yield return new TestCaseData(position, new BinaryFilePosition(new BinaryFile("Another", new byte[] { 1, 2, 3 }), 0, 1), false).SetName("Different file");
        yield return new TestCaseData(position, new BinaryFilePosition(binaryFile, 1, 1), false).SetName("Different start index");
        yield return new TestCaseData(position, new BinaryFilePosition(binaryFile, 0, 2), false).SetName("Different length");
        yield return new TestCaseData(position, null, false).SetName("Null");
        yield return new TestCaseData(position, new TextFilePosition(new TextFile("Test Name", " \t Test Line 0\n   Test Line 1"), 8, 20, 0, 8), false).SetName("Different SourceFilePosition type");
        yield return new TestCaseData(position, "Different", false).SetName("Different type");
    }
}