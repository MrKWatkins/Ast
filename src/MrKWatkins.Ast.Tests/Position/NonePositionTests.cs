using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class NonePositionTests : EqualityTestFixture
{
    [Test]
    public void Combine() => SourcePosition.None.Combine(SourcePosition.None).Should().BeSameAs(SourcePosition.None);

    [Test]
    public void CreateZeroWidthPrefix() => SourcePosition.None.CreateZeroWidthPrefix().Should().BeSameAs(SourcePosition.None);

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourcePosition x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        yield return new TestCaseData(SourcePosition.None, SourcePosition.None, true).SetName("Reference equals");
        yield return new TestCaseData(SourcePosition.None, null, false).SetName("Null");
        yield return new TestCaseData(SourcePosition.None, new BinaryFilePosition(new BinaryFile("Test", [1, 2, 3]), 0, 1), false).SetName("Different SourcePosition type");
        yield return new TestCaseData(SourcePosition.None, "Different", false).SetName("Different type");
    }
}