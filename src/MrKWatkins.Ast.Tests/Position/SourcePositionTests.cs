using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class SourcePositionTests
{
    [Test]
    public void Combine_DifferentTypes()
    {
        var positionX = new TestPosition();
        var positionY = new OtherPosition();

        positionX.Invoking(x => x.Combine(positionY))
            .Should().Throw<ArgumentException>()
            .WithMessage($"Value is not of type {nameof(TestPosition)}. (Parameter 'other')");

        positionY.Invoking(x => x.Combine(positionX))
            .Should().Throw<ArgumentException>()
            .WithMessage($"Value is not of type {nameof(OtherPosition)}. (Parameter 'other')");
    }
    
    [Test]
    public void Combine()
    {
        var combined = new TestPosition();
        var positionX = new TestPosition { Combined = combined };
        var positionY = new TestPosition();

        positionX.Combine(positionY).Should().BeSameAs(combined);
    }
    
    private sealed class TestPosition : SourcePosition<TestPosition>
    {
        protected override TestPosition Combine(TestPosition other) => Combined;

        public TestPosition Combined { get; init; } = null!;
    }
    
    private sealed class OtherPosition : SourcePosition<OtherPosition>
    {
        protected override OtherPosition Combine(OtherPosition other) => throw new NotSupportedException();
    }
}