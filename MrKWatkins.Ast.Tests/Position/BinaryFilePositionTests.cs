using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class BinaryFilePositionTests
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
}