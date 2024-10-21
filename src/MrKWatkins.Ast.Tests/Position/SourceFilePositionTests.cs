using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourceFilePositionTests
{
    [Test]
    public void Constructor_ThrowsIfStartIndexIsLessThanZero()
    {
        var file = new TestFile("Test Name", 10);
        FluentActions.Invoking(() => new TestPosition(file, -1, 5))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be 0 or greater. (Parameter 'startIndex'){Environment.NewLine}Actual value was -1.");
    }

    [Test]
    public void Constructor_ThrowsIfLengthIsLessThanZero()
    {
        var file = new TestFile("Test Name", 10);
        FluentActions.Invoking(() => new TestPosition(file, 0, -1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be 0 or greater. (Parameter 'length'){Environment.NewLine}Actual value was -1.");
    }

    [TestCase(10, 10)]
    [TestCase(10, 11)]
    public void Constructor_ThrowsIfStartIndexIsGreaterThanOrEqualToTheFileLength(int fileLength, int startIndex)
    {
        var file = new TestFile("Test Name", fileLength);
        FluentActions.Invoking(() => new TestPosition(file, startIndex, 1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be less than file's length. ({fileLength}) (Parameter 'startIndex'){Environment.NewLine}Actual value was {startIndex}.");
    }

    [Test]
    public void Constructor_ThrowsIfPositionExtendsOutsideTheFile()
    {
        var file = new TestFile("Test Name", 10);
        FluentActions.Invoking(() => new TestPosition(file, 8, 3))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value plus startIndex (8) must be less than file's length. (10) (Parameter 'length'){Environment.NewLine}Actual value was 3.");
    }

    [TestCase(10, 0, 0, 0)] // Zero length at start.
    [TestCase(10, 9, 0, 9)] // Zero length at end.
    [TestCase(10, 0, 10, 10)] // Full file.
    [TestCase(10, 3, 3, 6)] // Section.
    public void Constructor(int fileLength, int startIndex, int length, int expectedEndIndex)
    {
        var file = new TestFile("Test Name", fileLength);
        var position = new TestPosition(file, startIndex, length);
        position.File.Should().BeSameAs(file);
        position.StartIndex.Should().Be(startIndex);
        position.Length.Should().Be(length);
        position.EndIndex.Should().Be(expectedEndIndex);
    }

    [Test]
    public void Overlaps_DifferentFiles()
    {
        var positionX = new TestPosition(new TestFile("Test Name X", 10), 3, 4);
        var positionY = new TestPosition(new TestFile("Test Name Y", 10), 3, 4);

        positionX.Overlaps(positionY).Should().BeFalse();
        positionY.Overlaps(positionX).Should().BeFalse();
    }

    [TestCase(0, 0, 0, 0, false)]
    [TestCase(0, 10, 0, 0, false)]
    [TestCase(0, 10, 9, 0, false)]
    [TestCase(0, 10, 3, 3, true)]
    [TestCase(2, 2, 3, 3, true)]
    [TestCase(0, 0, 1, 1, false)]
    [TestCase(0, 5, 5, 0, false)]
    [TestCase(0, 5, 3, 0, true)]
    [TestCase(0, 2, 0, 2, true)]
    [TestCase(0, 2, 1, 2, true)]
    [TestCase(0, 2, 2, 2, false)]
    public void Overlaps(int startIndexX, int lengthX, int startIndexY, int lengthY, bool expected)
    {
        var file = new TestFile("Test Name", 10);
        var positionX = new TestPosition(file, startIndexX, lengthX);
        var positionY = new TestPosition(file, startIndexY, lengthY);

        positionX.Overlaps(positionY).Should().Be(expected);
        positionY.Overlaps(positionX).Should().Be(expected);
    }

    [Test]
    public void ToString_Test()
    {
        var file = new TestFile("Test Name", 10);
        var position = new TestPosition(file, 3, 2);
        position.ToString().Should().Be("Test Name (3, 2)");
    }

    [Test]
    public void Combine_DifferentFiles()
    {
        var positionX = new TestPosition(new TestFile("Test File X", 10), 0, 5);
        var positionY = new TestPosition(new TestFile("Test File Y", 10), 0, 5);

        positionX.Invoking(x => x.Combine(positionY))
            .Should().Throw<ArgumentException>()
            .WithMessage("Value is for a different file. (Parameter 'other')");

        positionY.Invoking(x => x.Combine(positionX))
            .Should().Throw<ArgumentException>()
            .WithMessage("Value is for a different file. (Parameter 'other')");
    }

    [Test]
    public void Combine()
    {
        var combined = new TestPosition(new TestFile("Test File", 10), 0, 5);
        var positionX = new TestPosition(new TestFile("Test File", 10), 0, 5) { Combined = combined };
        var positionY = new TestPosition(new TestFile("Test File", 10), 0, 5);

        positionX.Combine(positionY).Should().BeSameAs(combined);
    }

    [Test]
    public void Addition()
    {
        var combined = new TestPosition(new TestFile("Test File", 10), 0, 5);
        var positionX = new TestPosition(new TestFile("Test File", 10), 0, 5) { Combined = combined };
        var positionY = new TestPosition(new TestFile("Test File", 10), 0, 5);

        (positionX + positionY).Should().BeSameAs(combined);
    }

    private sealed class TestFile(string name, int length) : SourceFile(name, length);

    private sealed class TestPosition(TestFile file, int startIndex, int length) : SourceFilePosition<TestPosition, TestFile>(file, startIndex, length)
    {
        protected override TestPosition CreateCombination(TestPosition other) => Combined;

        public TestPosition Combined { get; init; } = null!;

        public override SourcePosition CreateZeroWidthPrefix() => throw new NotSupportedException();
    }
}