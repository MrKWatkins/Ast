using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourcePositionTests : EqualityTestFixture
{
    [Test]
    public void Combine_DifferentTypes()
    {
        var positionX = new TestPosition();
        var positionY = new OtherPosition();

        positionX.Invoking(x => x.Combine(positionY))
            .Should().Throw<ArgumentException>()
            .That.Should().HaveMessage($"Value is not of type {nameof(TestPosition)}. (Parameter 'other')");

        positionY.Invoking(x => x.Combine(positionX))
            .Should().Throw<ArgumentException>()
            .That.Should().HaveMessage($"Value is not of type {nameof(OtherPosition)}. (Parameter 'other')");
    }

    [Test]
    public void Combine()
    {
        var combined = new TestPosition();
        var positionX = new TestPosition { Combined = combined };
        var positionY = new TestPosition();

        positionX.Combine(positionY).Should().BeTheSameInstanceAs(combined);
    }

    [TestCaseSource(nameof(EqualityTestCases))]
    public void Equality(SourcePosition x, object? y, bool expected) => AssertEqual(x, y, expected);

    [Pure]
    public static IEnumerable<TestCaseData> EqualityTestCases()
    {
        var position = new TestPosition();

        yield return new TestCaseData(position, position, true).SetName("Reference equals");
        yield return new TestCaseData(position, null, false).SetName("Null");
        yield return new TestCaseData(position, new OtherPosition(), false).SetName("Different SourcePosition type");
        yield return new TestCaseData(position, "Different", false).SetName("Different type");
    }

    private sealed class TestPosition : SourcePosition<TestPosition>
    {
        protected override TestPosition Combine(TestPosition other) => Combined;

        public TestPosition Combined { get; init; } = null!;

        public override SourcePosition CreateZeroWidthPrefix() => throw new NotSupportedException();

        public override bool Equals(SourcePosition? other) => ReferenceEquals(this, other);
    }

    private sealed class OtherPosition : SourcePosition<OtherPosition>
    {
        protected override OtherPosition Combine(OtherPosition other) => throw new NotSupportedException();

        public override SourcePosition CreateZeroWidthPrefix() => throw new NotSupportedException();

        public override bool Equals(SourcePosition? other) => ReferenceEquals(this, other);
    }
}