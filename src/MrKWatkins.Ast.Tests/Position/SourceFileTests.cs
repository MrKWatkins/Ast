using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourceFileTests
{
    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_ThrowsIfLengthLessThanOrEqualToZero(int length) =>
        FluentActions.Invoking(() => new TestSourceFile("Test Name", length))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Value must be greater than 0. (Parameter 'length'){Environment.NewLine}Actual value was {length}.");

    [Test]
    public void Constructor()
    {
        var file = new TestSourceFile("Test Name", 100);
        file.Name.Should().Be("Test Name");
        file.Length.Should().Be(100);
    }
    
    [Test]
    public void ToString_Test()
    {
        var file = new TestSourceFile("Test Name", 100);
        file.ToString().Should().Be("Test Name");
    }
    
    
    [Test]
    public void Equality_SameReference()
    {
        var file = new TestSourceFile("Test Name", 100);
        AssertEqual(file, file, true);
    }
    
    [Test]
    public void Equality_SameName()
    {
        var x = new TestSourceFile("Test Name", 100);
        var y = new TestSourceFile("Test Name", 100);
        AssertEqual(x, y, true);
    }
    
    [Test]
    public void Equality_DifferentName()
    {
        var x = new TestSourceFile("Test Name 1", 100);
        var y = new TestSourceFile("Test Name 2", 100);
        AssertEqual(x, y, false);
    }
    
    [Test]
    public void Equality_DifferentLength()
    {
        var x = new TestSourceFile("Test Name", 100);
        var y = new TestSourceFile("Test Name", 200);
        AssertEqual(x, y, true);   // Equality ignores length, just checks name.
    }
    
    [Test]
    public void Equality_DifferentType()
    {
        var x = new TestSourceFile("Test Name", 100);
        var y = new OtherSourceFile("Test Name", 100);
        AssertEqual(x, y, false);
    }
    
    [Test]
    public void Equals_DifferentType()
    {
        var x = new TestSourceFile("Test Name", 100);
        object y = "Not A SourceFile";
        x.Equals(y).Should().BeFalse();
    }
    
    private static void AssertEqual(SourceFile x, SourceFile y, bool expected)
    {
        x.Equals(y).Should().Be(expected);
        y.Equals(x).Should().Be(expected);

        ((object) x).Equals(y).Should().Be(expected);
        ((object) y).Equals(x).Should().Be(expected);
        
        (x == y).Should().Be(expected);
        (y == x).Should().Be(expected);
        
        (x != y).Should().Be(!expected);
        (y != x).Should().Be(!expected);

        if (expected)
        {
            x.GetHashCode().Should().Be(y.GetHashCode());
            y.GetHashCode().Should().Be(x.GetHashCode());
        }
    }

    private sealed class TestSourceFile : SourceFile
    {
        public TestSourceFile(string name, int length) 
            : base(name, length)
        {
        }
    }
    
    private sealed class OtherSourceFile : SourceFile
    {
        public OtherSourceFile(string name, int length) 
            : base(name, length)
        {
        }
    }
}