using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourceFileTests : EqualityTestFixture
{
    [Test]
    public void Constructor_ThrowsIfLengthNegative() =>
        AssertThat.Invoking(() => new TestSourceFile("Test Name", -1))
            .Should().Throw<ArgumentOutOfRangeException>()
            .That.Should().HaveMessage($"Value must be greater than 0. (Parameter 'length'){Environment.NewLine}Actual value was {-1}.");

    [Test]
    public void Constructor()
    {
        var file = new TestSourceFile("Test Name", 100);
        file.Name.Should().Equal("Test Name");
        file.Length.Should().Equal(100);
    }

    [Test]
    public void ToString_Test()
    {
        var file = new TestSourceFile("Test Name", 100);
        file.ToString().Should().Equal("Test Name");
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

    private sealed class TestSourceFile(string name, int length) : SourceFile(name, length);

    private sealed class OtherSourceFile(string name, int length) : SourceFile(name, length);
}