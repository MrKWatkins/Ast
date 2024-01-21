using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourceFileTests : EqualityTestFixture
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

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourceFile x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var file = new TestSourceFile("Test Name", 100);

        yield return new TestCaseData(file, file, true).SetName("Reference equals");
        yield return new TestCaseData(file, new TestSourceFile("Test Name", 100), true).SetName("Value equals");
        yield return new TestCaseData(file, new TestSourceFile("Another Name", 100), false).SetName("Different name");
        yield return new TestCaseData(file, new TestSourceFile("Test Name", 101), true).SetName("Different length"); // Only name is checked.
        yield return new TestCaseData(file, null, false).SetName("Null");
        yield return new TestCaseData(file, new OtherSourceFile("Test Name", 100), false).SetName("Different SourceFile type");
        yield return new TestCaseData(file, "Different", false).SetName("Different type");
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