using System.Text;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class TextFilePositionTests
{
    [Test]
    public void Constructor_ThrowsIfStartLineIndexIsLessThanZero()
    {
        var file = new TextFile("Test Name", "Test Text");

        FluentActions.Invoking(() => new TextFilePosition(file, 0, 5, -1, 0))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be 0 or greater. (Parameter 'startLineIndex'){Environment.NewLine}Actual value was -1.");
    }
    
    [Test]
    public void Constructor_ThrowsIfStartColumnIndexIsLessThanZero()
    {
        var file = new TextFile("Test Name", "Test Text");

        FluentActions.Invoking(() => new TextFilePosition(file, 0, 5, 0, -1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be 0 or greater. (Parameter 'startColumnIndex'){Environment.NewLine}Actual value was -1.");
    }
    
    [TestCase(2)]
    [TestCase(3)]
    public void Constructor_ThrowsIfStartLineIndexIsGreaterThanOrEqualToTheNumberOfLines(int startLineIndex)
    {
        var file = new TextFile("Test Name", $"Test Line 0{Environment.NewLine}Test Line 1");

        FluentActions.Invoking(() => new TextFilePosition(file, 0, 5, startLineIndex, 0))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be less than the number of lines in file. (2) (Parameter 'startLineIndex'){Environment.NewLine}Actual value was {startLineIndex}.");
    }
    
    [TestCase(11)]
    [TestCase(12)]
    public void Constructor_ThrowsIfStartColumnIndexIsGreaterThanOrEqualToTheLengthOfTheLine(int startColumnIndex)
    {
        var file = new TextFile("Test Name", $"Test Line 0{Environment.NewLine}Test Line 1");

        FluentActions.Invoking(() => new TextFilePosition(file, 0, 5, 1, startColumnIndex))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be less than the length of the start line. (11) (Parameter 'startColumnIndex'){Environment.NewLine}Actual value was {startColumnIndex}.");
    }

    [Test]
    public void Constructor()
    {
        var textFile = new TextFile("Test Filename", "Some Text\nSome More Text");

        var position = new TextFilePosition(textFile, 15, 4, 1, 5);
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

    [TestCase(
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 0, 0, 0)]
    [TestCase(
        0, 0, 0, 0,
        22, 0, 1, 10,
        0, 22, 0, 0)]
    [TestCase(
        0, 11, 0, 0,
        12, 11, 1, 0,
        0, 23, 0, 0)]
    [TestCase(
        5, 4, 0, 5,
        18, 4, 1, 5,
        5, 17, 0, 5)]
    public void CreateCombination(
        int startIndexX, int lengthX, int startLineIndexX, int startColumnIndexX, 
        int startIndexY, int lengthY, int startLineIndexY, int startColumnIndexY, 
        int expectedStartIndex, int expectedLength, int expectedStartLineIndex, int expectedStartColumnIndex)
    {
        var file = new TextFile("Test Name", "Test Line 0\nTest Line 1");

        var positionX = new TextFilePosition(file, startIndexX, lengthX, startLineIndexX, startColumnIndexX);
        var positionY = new TextFilePosition(file, startIndexY, lengthY, startLineIndexY, startColumnIndexY);

        var combined = positionX.Combine(positionY);
        combined.StartIndex.Should().Be(expectedStartIndex);
        combined.Length.Should().Be(expectedLength);
        combined.StartLineIndex.Should().Be(expectedStartLineIndex);
        combined.StartColumnIndex.Should().Be(expectedStartColumnIndex);
        
        combined = positionY.Combine(positionX);
        combined.StartIndex.Should().Be(expectedStartIndex);
        combined.Length.Should().Be(expectedLength);
        combined.StartLineIndex.Should().Be(expectedStartLineIndex);
        combined.StartColumnIndex.Should().Be(expectedStartColumnIndex);
    }

    [Test]
    public void ToString_Test()
    {
        var file = new TextFile("Test Name", "Test Line 0\nTest Line 1");
        var position = new TextFilePosition(file, 18, 4, 1, 5);
        position.ToString().Should().Be("Test Name (2, 6)");
    }

    [Test]
    public void WriteSourceForMessage_SingleLine()
    {
        var file = new TextFile("Test Name", " \t Test Line 0\n   Test Line 1");
        var position = new TextFilePosition(file, 8, 4, 0, 8);

        var stringBuilder = new StringBuilder();
        position.WriteSourceForMessage(stringBuilder);
        stringBuilder.ToString().Should().BeEquivalentTo($" \t Test Line 0{Environment.NewLine} \t      ----");
    }

    [Test]
    public void WriteSourceForMessage_MultiLine()
    {
        var file = new TextFile("Test Name", " \t Test Line 0\n   Test Line 1");
        var position = new TextFilePosition(file, 8, 20, 0, 8);

        var stringBuilder = new StringBuilder();
        position.WriteSourceForMessage(stringBuilder);
        stringBuilder.ToString().Should().BeEquivalentTo($" \t Test Line 0{Environment.NewLine} \t      ------");
    }
}